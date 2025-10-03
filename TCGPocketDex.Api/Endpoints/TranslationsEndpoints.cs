using Microsoft.AspNetCore.Mvc;
using TCGPocketDex.Api.Services;
using TCGPocketDex.Contracts.DTO;

namespace TCGPocketDex.Api.Endpoints;

public static class TranslationsEndpoints
{
    public static IEndpointRouteBuilder MapTranslationEndpoints(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app.MapGroup("/cards/{cardId:int}/translations").RequireAuthorization();

        group.MapPost("", AddCardTranslationAsync)
        .WithName("AddCardTranslation")
        .WithOpenApi();

        return app;
    }

    private static async Task<IResult> AddCardTranslationAsync(
        [FromRoute] int cardId,
        [FromBody] CardTranslationInputDTO dto,
        ICardService service,
        CancellationToken ct)
    {
        try
        {
            await service.AddCardTranslationAsync(cardId, dto, ct);
            return Results.Created($"/cards/{cardId}/translations", null);
        }
        catch (ArgumentException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }
    }
}
