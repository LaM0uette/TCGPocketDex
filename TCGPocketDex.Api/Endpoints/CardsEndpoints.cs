using Microsoft.EntityFrameworkCore;
using TCGPocketDex.Api.Data;
using TCGPocketDex.Api.Entities;
using TCGPocketDex.Api.Mappings;
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
            await service.CreatePokemonAsync(dto, ct);
            return Results.Created($"/cards/pokemon", null); // 201 sans body
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
            await service.CreateFossilAsync(dto, ct);
            return Results.Created($"/cards/fossil", null);
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
            await service.CreateToolAsync(dto, ct);
            return Results.Created($"/cards/tool", null);
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
            await service.CreateItemAsync(dto, ct);
            return Results.Created($"/cards/item", null);
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
            await service.CreateSupporterAsync(dto, ct);
            return Results.Created($"/cards/supporter", null);
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
        List<Card> cards = await db.Cards
            .AsNoTracking()
            .Include(c => c.Type)
            .Include(c => c.Rarity)
            .Include(c => c.Collection)
            .Include(c => c.Specials)
            .Include(c => c.Translations)
            .ToListAsync(ct);
        List<CardOutputDTO> dtos = cards.Select(c => c.ToOutputDTO()).ToList();
        
        return Results.Ok(dtos);
    }

    private static async Task<IResult> GetCardByIdAsync(int id, ApplicationDbContext db, CancellationToken ct)
    {
        Card? card = await db.Cards
            .AsNoTracking()
            .Include(c => c.Type)
            .Include(c => c.Rarity)
            .Include(c => c.Collection)
            .Include(c => c.Specials)
            .Include(c => c.Translations)
            .FirstOrDefaultAsync(c => c.Id == id, ct);
        if (card is null)
            return Results.NotFound();
        
        CardOutputDTO dto = card.ToOutputDTO();
        return Results.Ok(dto);
    }
    
    #endregion
}
