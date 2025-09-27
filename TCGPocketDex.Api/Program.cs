using Microsoft.EntityFrameworkCore;
using TCGPocketDex.Api.Data;

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

// Add services to the container.
builder.Services.AddAuthorization();

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

string connectionString = builder.Configuration.GetConnectionString("Default") ?? throw new InvalidOperationException("ConnectionStrings: Default is not configured in appsettings.json or environment.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});

// Repositories
//builder.Services.AddScoped<IUserRepository, UserRepository>();

// Services
//builder.Services.AddScoped<IUserService, UserService>();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();
