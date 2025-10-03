using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
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

// JWT Authentication — only the SDK (holding the private key) can call the API
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    string? publicKeyPem = builder.Configuration["Jwt:PublicKeyPem"] ?? Environment.GetEnvironmentVariable("TCGPDK_JWT_PUBLIC_KEY_PEM");
    if (string.IsNullOrWhiteSpace(publicKeyPem))
    {
        // In dev, allow empty to avoid immediate crash; but all protected endpoints will still 401.
        publicKeyPem = "";
    }

    string validIssuer = builder.Configuration["Jwt:Issuer"] ?? Environment.GetEnvironmentVariable("TCGPDK_JWT_ISSUER") ?? "TCGPocketDex.SDK";
    string validAudience = builder.Configuration["Jwt:Audience"] ?? Environment.GetEnvironmentVariable("TCGPDK_JWT_AUDIENCE") ?? "TCGPocketDex.Api";

    SecurityKey? signingKey = null;
    if (!string.IsNullOrWhiteSpace(publicKeyPem))
    {
        RSA rsa = RSA.Create();
        rsa.ImportFromPem(publicKeyPem);
        signingKey = new RsaSecurityKey(rsa);
    }

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = signingKey ?? new SymmetricSecurityKey(new byte[32]), // placeholder to avoid null; token will still fail validation if empty
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = validIssuer,
        ValidAudience = validAudience,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromSeconds(30)
    };
});

builder.Services.AddAuthorization();

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
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();

app.MapCardEndpoints();
app.MapTranslationEndpoints();

app.Run();
