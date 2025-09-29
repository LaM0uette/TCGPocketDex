using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using TCGPocketDex.Api.Old.Services;
using TCGPocketDex.Contracts.References;

namespace TCGPocketDex.Api.Old.Endpoints;

public static class TypesEndpoints
{
    public static IEndpointRouteBuilder MapPokemonTypes(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/pokemon-types");

        group.MapGet("", async (IPokemonTypeService svc, string culture, CancellationToken ct) =>
        {
            var result = await svc.GetAllAsync(culture, ct);
            return Results.Ok(result);
        });

        group.MapPost("", async (IPokemonTypeService svc, PokemonTypeInputDTO input, CancellationToken ct) =>
        {
            var created = await svc.CreateAsync(input, ct);
            return Results.Created($"/pokemon-types/{created.Id}", created);
        });

        return app;
    }
}
