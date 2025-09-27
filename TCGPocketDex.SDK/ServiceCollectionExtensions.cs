using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TCGPocketDex.SDK.Cards;
using TCGPocketDex.SDK.References;

namespace TCGPocketDex.SDK;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTcgpocketDexSdk(this IServiceCollection services, IConfiguration configuration)
    {
        var apiBaseUrl = configuration["ApiBaseUrl"] ?? throw new InvalidOperationException("ApiBaseUrl is not configured.");
        services.AddHttpClient<ICardsClient, CardsClient>(client =>
        {
            client.BaseAddress = new Uri(apiBaseUrl);
        });
        services.AddHttpClient<ITypesClient, TypesClient>(client =>
        {
            client.BaseAddress = new Uri(apiBaseUrl);
        });
        services.AddHttpClient<IStagesClient, StagesClient>(client =>
        {
            client.BaseAddress = new Uri(apiBaseUrl);
        });
        services.AddHttpClient<IAbilitiesClient, AbilitiesClient>(client =>
        {
            client.BaseAddress = new Uri(apiBaseUrl);
        });
        services.AddHttpClient<IAttacksClient, AttacksClient>(client =>
        {
            client.BaseAddress = new Uri(apiBaseUrl);
        });
        services.AddHttpClient<ICardExtensionsClient, CardExtensionsClient>(client =>
        {
            client.BaseAddress = new Uri(apiBaseUrl);
        });
        services.AddHttpClient<IBoostersClient, BoostersClient>(client =>
        {
            client.BaseAddress = new Uri(apiBaseUrl);
        });
        services.AddHttpClient<IRaritiesClient, RaritiesClient>(client =>
        {
            client.BaseAddress = new Uri(apiBaseUrl);
        });
        return services;
    }
}
