using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using TCGPocketDex.Api.Services;
using TCGPocketDex.Contracts.Cards;

namespace TCGPocketDex.Api.Endpoints;

public static class CardsEndpoints
{
    public static IEndpointRouteBuilder MapCardEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/cards/pokemon", async (ICardService service, CardPokemonInputDTO dto, CancellationToken ct) =>
        {
            try
            {
                var result = await service.CreatePokemonAsync(dto, ct);
                return Results.Created($"/cards/{result.Id}", result);
            }
            catch (ArgumentException ex)
            {
                return Results.BadRequest(new { error = ex.Message });
            }
        });

        app.MapPost("/cards/fossil", async (ICardService service, CardFossilInputDTO dto, CancellationToken ct) =>
        {
            try
            {
                var result = await service.CreateFossilAsync(dto, ct);
                return Results.Created($"/cards/{result.Id}", result);
            }
            catch (ArgumentException ex)
            {
                return Results.BadRequest(new { error = ex.Message });
            }
        });

        app.MapPost("/cards/tool", async (ICardService service, CardToolInputDTO dto, CancellationToken ct) =>
        {
            try
            {
                var result = await service.CreateToolAsync(dto, ct);
                return Results.Created($"/cards/{result.Id}", result);
            }
            catch (ArgumentException ex)
            {
                return Results.BadRequest(new { error = ex.Message });
            }
        });

        app.MapPost("/cards/item", async (ICardService service, CardItemInputDTO dto, CancellationToken ct) =>
        {
            try
            {
                var result = await service.CreateItemAsync(dto, ct);
                return Results.Created($"/cards/{result.Id}", result);
            }
            catch (ArgumentException ex)
            {
                return Results.BadRequest(new { error = ex.Message });
            }
        });

        app.MapPost("/cards/supporter", async (ICardService service, CardSupporterInputDTO dto, CancellationToken ct) =>
        {
            try
            {
                var result = await service.CreateSupporterAsync(dto, ct);
                return Results.Created($"/cards/{result.Id}", result);
            }
            catch (ArgumentException ex)
            {
                return Results.BadRequest(new { error = ex.Message });
            }
        });

        return app;
    }
}
