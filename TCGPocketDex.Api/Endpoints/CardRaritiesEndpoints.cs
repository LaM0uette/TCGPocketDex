using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using TCGPocketDex.Api.Services;
using TCGPocketDex.Contracts.References;

namespace TCGPocketDex.Api.Endpoints;

public static class CardRaritiesEndpoints
{
    public static IEndpointRouteBuilder MapCardRarities(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/card-rarities");

        group.MapGet("", async (ICardRarityService svc, CancellationToken ct) =>
        {
            var result = await svc.GetAllAsync(ct);
            return Results.Ok(result);
        });

        group.MapPost("", async (ICardRarityService svc, CardRarityInputDTO input, CancellationToken ct) =>
        {
            var created = await svc.CreateAsync(input, ct);
            return Results.Created($"/card-rarities/{created.Id}", created);
        });

        return app;
    }
}
