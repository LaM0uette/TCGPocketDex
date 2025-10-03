using Microsoft.EntityFrameworkCore;
using TCGPocketDex.Api.Data;
using TCGPocketDex.Api.Entity;
using TCGPocketDex.Api.Services;
using TCGPocketDex.Contracts.DTO;

namespace TCGPocketDex.Api.Endpoints;

public static class CardsEndpoints
{
    #region Statements

    public static IEndpointRouteBuilder MapCardEndpoints(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app.MapGroup("/cards").WithTags("Cards");

        group.MapPost("/pokemon", CreateCardPokemonAsync);
        group.MapPost("/fossil", CreateCardFossilAsync);
        group.MapPost("/tool", CreateCardToolAsync);
        group.MapPost("/item", CreateCardItemAsync);
        group.MapPost("/supporter", CreateCardSupporterAsync);
        
        group.MapGet("/", GetAllCardsAsync);
        group.MapGet("/{id:int}", GetCardByIdAsync);
        
        return app;
    }

    #endregion

    #region MyRegion

    private static async Task<IResult> CreateCardPokemonAsync(ICardService service, CardPokemonInputDTO dto, CancellationToken ct)
    {
        try
        {
            CardPokemonOutputDTO result = await service.CreatePokemonAsync(dto, ct);
            return Results.Created($"/cards/{result.Id}", result);
        }
        catch (ArgumentException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }
    }

    private static async Task<IResult> CreateCardFossilAsync(ICardService service, CardFossilInputDTO dto, CancellationToken ct)
    {
        try
        {
            CardFossilOutputDTO result = await service.CreateFossilAsync(dto, ct);
            return Results.Created($"/cards/{result.Id}", result);
        }
        catch (ArgumentException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }
    }

    private static async Task<IResult> CreateCardToolAsync(ICardService service, CardToolInputDTO dto, CancellationToken ct)
    {
        try
        {
            CardToolOutputDTO result = await service.CreateToolAsync(dto, ct);
            return Results.Created($"/cards/{result.Id}", result);
        }
        catch (ArgumentException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }
    }

    private static async Task<IResult> CreateCardItemAsync(ICardService service, CardItemInputDTO dto, CancellationToken ct)
    {
        try
        {
            CardItemOutputDTO result = await service.CreateItemAsync(dto, ct);
            return Results.Created($"/cards/{result.Id}", result);
        }
        catch (ArgumentException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }
    }

    private static async Task<IResult> CreateCardSupporterAsync(ICardService service, CardSupporterInputDTO dto, CancellationToken ct)
    {
        try
        {
            CardSupporterOutputDTO result = await service.CreateSupporterAsync(dto, ct);
            return Results.Created($"/cards/{result.Id}", result);
        }
        catch (ArgumentException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }
    }

    #endregion
    
    #region Read
    
    private static async Task<IResult> GetAllCardsAsync(ApplicationDbContext db, CancellationToken ct)
    {
        List<Card> cards = await db.Cards.AsNoTracking().ToListAsync(ct);
        return Results.Ok(cards);
    }

    private static async Task<IResult> GetCardByIdAsync(int id, ApplicationDbContext db, CancellationToken ct)
    {
        Card? card = await db.Cards.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id, ct);
        return card is not null ? Results.Ok(card) : Results.NotFound();
    }
    
    #endregion
}
