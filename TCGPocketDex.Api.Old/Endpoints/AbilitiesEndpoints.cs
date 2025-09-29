using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using TCGPocketDex.Api.Old.Services;
using TCGPocketDex.Contracts.References;

namespace TCGPocketDex.Api.Old.Endpoints;

public static class AbilitiesEndpoints
{
    public static IEndpointRouteBuilder MapPokemonAbilities(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/pokemon-abilities");

        group.MapGet("", async (IPokemonAbilityService svc, string culture, CancellationToken ct) =>
        {
            var result = await svc.GetAllAsync(culture, ct);
            return Results.Ok(result);
        });

        group.MapPost("", async (IPokemonAbilityService svc, PokemonAbilityInputDTO input, CancellationToken ct) =>
        {
            var created = await svc.CreateAsync(input, ct);
            return Results.Created($"/pokemon-abilities/{created.Id}", created);
        });

        return app;
    }
}
