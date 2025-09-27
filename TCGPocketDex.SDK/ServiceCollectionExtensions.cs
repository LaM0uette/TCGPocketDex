using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TCGPocketDex.SDK.Cards;

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
        return services;
    }
}
