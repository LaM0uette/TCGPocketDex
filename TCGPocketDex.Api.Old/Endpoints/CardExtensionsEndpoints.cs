using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using TCGPocketDex.Api.Old.Services;
using TCGPocketDex.Contracts.References;

namespace TCGPocketDex.Api.Old.Endpoints;

public static class CardExtensionsEndpoints
{
    public static IEndpointRouteBuilder MapCardExtensions(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/card-extensions");

        group.MapGet("", async (ICardExtensionService svc, string culture, CancellationToken ct) =>
        {
            var result = await svc.GetAllAsync(culture, ct);
            return Results.Ok(result);
        });

        group.MapPost("", async (ICardExtensionService svc, CardExtensionInputDTO input, CancellationToken ct) =>
        {
            var created = await svc.CreateAsync(input, ct);
            return Results.Created($"/card-extensions/{created.Id}", created);
        });

        return app;
    }
}
