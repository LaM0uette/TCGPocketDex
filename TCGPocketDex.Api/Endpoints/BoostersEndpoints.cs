using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using TCGPocketDex.Api.Services;
using TCGPocketDex.Contracts.References;

namespace TCGPocketDex.Api.Endpoints;

public static class BoostersEndpoints
{
    public static IEndpointRouteBuilder MapBoosters(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/boosters");

        group.MapGet("", async (IBoosterService svc, string culture, int? cardExtensionId, CancellationToken ct) =>
        {
            var result = await svc.GetAllAsync(culture, cardExtensionId, ct);
            return Results.Ok(result);
        });

        group.MapPost("", async (IBoosterService svc, BoosterInputDTO input, CancellationToken ct) =>
        {
            var created = await svc.CreateAsync(input, ct);
            return Results.Created($"/boosters/{created.Id}", created);
        });

        return app;
    }
}
