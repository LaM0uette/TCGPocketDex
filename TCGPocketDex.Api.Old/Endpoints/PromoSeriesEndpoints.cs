using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using TCGPocketDex.Api.Old.Services;
using TCGPocketDex.Contracts.References;

namespace TCGPocketDex.Api.Old.Endpoints;

public static class PromoSeriesEndpoints
{
    public static IEndpointRouteBuilder MapPromoSeries(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/promo-series");

        group.MapGet("", async (IPromoSeriesService svc, string culture, CancellationToken ct) =>
        {
            var result = await svc.GetAllAsync(culture, ct);
            return Results.Ok(result);
        });

        group.MapPost("", async (IPromoSeriesService svc, PromoSeriesInputDTO input, CancellationToken ct) =>
        {
            var created = await svc.CreateAsync(input, ct);
            return Results.Created($"/promo-series/{created.Id}", created);
        });

        return app;
    }
}
