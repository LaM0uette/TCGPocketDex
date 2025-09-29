using Microsoft.AspNetCore.Mvc;
using TCGPocketDex.Api.Services;
using TCGPocketDex.Contracts.Cards;

namespace TCGPocketDex.Api.Endpoints;

public static class TranslationsEndpoints
{
    public static IEndpointRouteBuilder MapTranslationEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/cards/{cardId:int}/translations");

        group.MapPost("", async ([FromRoute] int cardId, [FromBody] CardTranslationInputDTO dto, ICardService service, CancellationToken ct) =>
        {
            try
            {
                var result = await service.AddCardTranslationAsync(cardId, dto, ct);
                return Results.Created($"/cards/{cardId}/translations/{result.Id}", result);
            }
            catch (ArgumentException ex)
            {
                return Results.BadRequest(new { error = ex.Message });
            }
        })
        .WithName("AddCardTranslation")
        .WithOpenApi();

        return app;
    }
}
