using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using TCGPocketDex.Api.Old.Services;
using TCGPocketDex.Contracts.Cards;

namespace TCGPocketDex.Api.Old.Endpoints;

public static class CardsEndpoints
{
    public static IEndpointRouteBuilder MapCards(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/cards");

        group.MapGet("", async (ICardService svc, string culture, CancellationToken ct) =>
        {
            var result = await svc.GetAllAsync(culture, ct);
            return Results.Ok(result);
        });

        group.MapGet("/{id:int}", async (ICardService svc, int id, string culture, CancellationToken ct) =>
        {
            var result = await svc.GetByIdAsync(id, culture, ct);
            return result == null ? Results.NotFound() : Results.Ok(result);
        });

        group.MapPost("", async (ICardService svc, CardInputDTO input, CancellationToken ct) =>
        {
            var created = await svc.CreateAsync(input, ct);
            return Results.Created($"/cards/{created.Id}", created);
        });

        group.MapPost("/{id:int}/translations", async (ICardService svc, int id, CardTranslationInputDTO input, CancellationToken ct) =>
        {
            var updated = await svc.AddTranslationAsync(id, input, ct);
            return updated == null ? Results.NotFound() : Results.Ok(updated);
        });

        group.MapPut("/{id:int}", async (ICardService svc, int id, CardInputDTO input, CancellationToken ct) =>
        {
            var updated = await svc.UpdateAsync(id, input, ct);
            return updated == null ? Results.NotFound() : Results.Ok(updated);
        });

        group.MapDelete("/{id:int}", async (ICardService svc, int id, CancellationToken ct) =>
        {
            var ok = await svc.DeleteAsync(id, ct);
            return ok ? Results.NoContent() : Results.NotFound();
        });

        return app;
    }
}
