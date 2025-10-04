using Microsoft.EntityFrameworkCore;
using TCGPocketDex.Api.Data;
using TCGPocketDex.Api.Endpoints;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

string[] allowedOrigins =
[
    "http://localhost:5277",
    "https://localhost:7164",
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

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AppPolicy");

// Redirect root to Swagger UI so it's opened by default
app.MapGet("/", () => Results.Redirect("/swagger"));

app.UseHttpsRedirection();
app.UseStaticFiles();

app.MapCardEndpoints();
app.MapTranslationEndpoints();

app.Run();
