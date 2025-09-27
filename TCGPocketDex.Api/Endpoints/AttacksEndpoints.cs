using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using TCGPocketDex.Api.Services;
using TCGPocketDex.Contracts.References;

namespace TCGPocketDex.Api.Endpoints;

public static class AttacksEndpoints
{
    public static IEndpointRouteBuilder MapPokemonAttacks(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/pokemon-attacks");

        group.MapGet("", async (IPokemonAttackService svc, string culture, CancellationToken ct) =>
        {
            var result = await svc.GetAllAsync(culture, ct);
            return Results.Ok(result);
        });

        group.MapPost("", async (IPokemonAttackService svc, PokemonAttackInputDTO input, CancellationToken ct) =>
        {
            var created = await svc.CreateAsync(input, ct);
            return Results.Created($"/pokemon-attacks/{created.Id}", created);
        });

        return app;
    }
}
