using Microsoft.EntityFrameworkCore;
using TCGPocketDex.Api.Old.Data;
using TCGPocketDex.Api.Old.Endpoints;
using TCGPocketDex.Api.Old.Repositories;
using TCGPocketDex.Api.Old.Services;

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


builder.Services.AddScoped<ICardRepository, CardRepository>();
builder.Services.AddScoped<ICardService, CardService>();
builder.Services.AddScoped<IPokemonTypeRepository, PokemonTypeRepository>();
builder.Services.AddScoped<IPokemonTypeService, PokemonTypeService>();
builder.Services.AddScoped<IPokemonStageRepository, PokemonStageRepository>();
builder.Services.AddScoped<IPokemonStageService, PokemonStageService>();

builder.Services.AddScoped<IPokemonAbilityRepository, PokemonAbilityRepository>();
builder.Services.AddScoped<IPokemonAbilityService, PokemonAbilityService>();

builder.Services.AddScoped<IPokemonAttackRepository, PokemonAttackRepository>();
builder.Services.AddScoped<IPokemonAttackService, PokemonAttackService>();

builder.Services.AddScoped<ICardExtensionRepository, CardExtensionRepository>();
builder.Services.AddScoped<ICardExtensionService, CardExtensionService>();

builder.Services.AddScoped<IBoosterRepository, BoosterRepository>();
builder.Services.AddScoped<IBoosterService, BoosterService>();

builder.Services.AddScoped<ICardRarityRepository, CardRarityRepository>();
builder.Services.AddScoped<ICardRarityService, CardRarityService>();

builder.Services.AddScoped<IPromoSeriesRepository, PromoSeriesRepository>();
builder.Services.AddScoped<IPromoSeriesService, PromoSeriesService>();


string connectionString = builder.Configuration.GetConnectionString("Default") ?? throw new InvalidOperationException("ConnectionStrings: Default is not configured in appsettings.json or environment.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});

WebApplication app = builder.Build();

// Apply pending EF Core migrations automatically at startup
/*
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}
*/

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
app.UseStaticFiles();

app.MapCards();
app.MapPokemonTypes();
app.MapPokemonStages();
app.MapPokemonAbilities();
app.MapPokemonAttacks();
app.MapCardExtensions();
app.MapBoosters();
app.MapCardRarities();
app.MapPromoSeries();
app.MapImages();

app.Run();
