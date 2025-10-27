using System.IO.Compression;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using TCGPocketDex.Api.Data;
using TCGPocketDex.Api.Endpoints;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

string[] allowedOrigins =
[
    "http://localhost:5277",
    "https://localhost:7164",
    "https://localhost:7184",
    "https://0.0.0.1"
];

builder.Services.AddCors(options =>
{
    options.AddPolicy("AppPolicy", policy =>
    {
        policy.WithOrigins(allowedOrigins)
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


// Brotli and Gzip compression
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<BrotliCompressionProvider>();
    options.Providers.Add<GzipCompressionProvider>();
});
builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.Fastest;
});
builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.Fastest;
});

// Json optimizations
builder.Services.AddControllers()
    .AddJsonOptions(o =>
    {
        o.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
        o.JsonSerializerOptions.DefaultIgnoreCondition = 
            System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
        o.JsonSerializerOptions.WriteIndented = false; // compact
    });


// DI registrations
builder.Services.AddScoped<TCGPocketDex.Api.Repositories.ICardRepository, TCGPocketDex.Api.Repositories.CardRepository>();
builder.Services.AddScoped<TCGPocketDex.Api.Services.ICardService, TCGPocketDex.Api.Services.CardService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

string connectionString = builder.Configuration.GetConnectionString("Default") ?? throw new InvalidOperationException("ConnectionStrings: Default is not configured in appsettings.json or environment.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});

// EF Core tracking disabled by default for queries (you do not modify entities from your GETs)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(connectionString);
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AppPolicy");
app.UseResponseCompression();

// Redirect root to Swagger UI so it's opened by default
app.MapGet("/", () => Results.Redirect("/swagger"));

app.UseHttpsRedirection();
app.UseStaticFiles();

app.MapCardEndpoints();
app.MapTranslationEndpoints();

app.Run();
