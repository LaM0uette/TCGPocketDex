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

builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

builder.Services.AddScoped<TCGPocketDex.Api.Repositories.ICardRepository, TCGPocketDex.Api.Repositories.CardRepository>();
builder.Services.AddScoped<TCGPocketDex.Api.Services.ICardService, TCGPocketDex.Api.Services.CardService>();
builder.Services.AddScoped<TCGPocketDex.Api.Repositories.IPokemonTypeRepository, TCGPocketDex.Api.Repositories.PokemonTypeRepository>();
builder.Services.AddScoped<TCGPocketDex.Api.Services.IPokemonTypeService, TCGPocketDex.Api.Services.PokemonTypeService>();
builder.Services.AddScoped<TCGPocketDex.Api.Repositories.IPokemonStageRepository, TCGPocketDex.Api.Repositories.PokemonStageRepository>();
builder.Services.AddScoped<TCGPocketDex.Api.Services.IPokemonStageService, TCGPocketDex.Api.Services.PokemonStageService>();

builder.Services.AddScoped<TCGPocketDex.Api.Repositories.IPokemonAbilityRepository, TCGPocketDex.Api.Repositories.PokemonAbilityRepository>();
builder.Services.AddScoped<TCGPocketDex.Api.Services.IPokemonAbilityService, TCGPocketDex.Api.Services.PokemonAbilityService>();

builder.Services.AddScoped<TCGPocketDex.Api.Repositories.IPokemonAttackRepository, TCGPocketDex.Api.Repositories.PokemonAttackRepository>();
builder.Services.AddScoped<TCGPocketDex.Api.Services.IPokemonAttackService, TCGPocketDex.Api.Services.PokemonAttackService>();

builder.Services.AddScoped<TCGPocketDex.Api.Repositories.ICardExtensionRepository, TCGPocketDex.Api.Repositories.CardExtensionRepository>();
builder.Services.AddScoped<TCGPocketDex.Api.Services.ICardExtensionService, TCGPocketDex.Api.Services.CardExtensionService>();

builder.Services.AddScoped<TCGPocketDex.Api.Repositories.IBoosterRepository, TCGPocketDex.Api.Repositories.BoosterRepository>();
builder.Services.AddScoped<TCGPocketDex.Api.Services.IBoosterService, TCGPocketDex.Api.Services.BoosterService>();

builder.Services.AddScoped<TCGPocketDex.Api.Repositories.ICardRarityRepository, TCGPocketDex.Api.Repositories.CardRarityRepository>();
builder.Services.AddScoped<TCGPocketDex.Api.Services.ICardRarityService, TCGPocketDex.Api.Services.CardRarityService>();

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
app.UseAuthorization();

app.MapCards();
app.MapPokemonTypes();
app.MapPokemonStages();
app.MapPokemonAbilities();
app.MapPokemonAttacks();
app.MapCardExtensions();
app.MapBoosters();
app.MapCardRarities();

app.Run();
