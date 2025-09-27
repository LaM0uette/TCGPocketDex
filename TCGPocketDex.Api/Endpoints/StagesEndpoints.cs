using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using TCGPocketDex.Api.Services;
using TCGPocketDex.Contracts.References;

namespace TCGPocketDex.Api.Endpoints;

public static class StagesEndpoints
{
    public static IEndpointRouteBuilder MapPokemonStages(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/pokemon-stages");

        group.MapGet("", async (IPokemonStageService svc, string culture, CancellationToken ct) =>
        {
            var result = await svc.GetAllAsync(culture, ct);
            return Results.Ok(result);
        });

        group.MapPost("", async (IPokemonStageService svc, PokemonStageInputDTO input, CancellationToken ct) =>
        {
            var created = await svc.CreateAsync(input, ct);
            return Results.Created($"/pokemon-stages/{created.Id}", created);
        });

        return app;
    }
}
