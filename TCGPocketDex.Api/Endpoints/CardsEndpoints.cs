using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using TCGPocketDex.Api.Data;
using TCGPocketDex.Api.Entities;
using TCGPocketDex.Api.Mappings;
using TCGPocketDex.Api.Services;
using TCGPocketDex.Contracts.DTO;
using TCGPocketDex.Contracts.Request;

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
        group.MapGet("/{collectionCode}/{collectionNumber:int}", GetCardByCollectionNumberAsync);
        group.MapPost("/batch", GetCardsByBatchAsync);
        
        return app;
    }

    #endregion

    #region Create

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
    
    private static async Task<IResult> GetAllCardsAsync(ApplicationDbContext db, HttpContext http, CancellationToken ct)
    {
        string resolvedCulture = ResolveCulture(http);

        List<Card> cards = await db.Cards
            .AsNoTracking()
            .AsSplitQuery()
            .WithAllIncludes()
            .ToListAsync(ct);
        
        List<CardOutputDTO> dtos = cards.Select(c => c.ToOutputDTOWithCulture(resolvedCulture)).ToList();
        return Results.Ok(dtos);
    }

    private static async Task<IResult> GetCardByIdAsync(int id, ApplicationDbContext db, HttpContext http, CancellationToken ct)
    {
        string resolvedCulture = ResolveCulture(http);

        Card? card = await db.Cards
            .AsNoTracking()
            .AsSplitQuery()
            .WithAllIncludes()
            .FirstOrDefaultAsync(c => c.Id == id, ct);
        if (card is null)
            return Results.NotFound();
        
        CardOutputDTO dto = card.ToOutputDTOWithCulture(resolvedCulture);
        return Results.Ok(dto);
    }

    private static async Task<IResult> GetCardByCollectionNumberAsync(string collectionCode, int collectionNumber, ApplicationDbContext db, HttpContext http, CancellationToken ct)
    {
        string resolvedCulture = ResolveCulture(http);

        Card? card = await db.Cards
            .AsNoTracking()
            .AsSplitQuery()
            .WithAllIncludes()
            .FirstOrDefaultAsync(c => c.Collection.Code == collectionCode && c.CollectionNumber == collectionNumber, ct);
        
        if (card is null)
            return Results.NotFound();

        CardOutputDTO dto = card.ToOutputDTOWithCulture(resolvedCulture);
        return Results.Ok(dto);
    }

    private static async Task<IResult> GetCardsByBatchAsync(
        DeckRequest deck,
        ApplicationDbContext db,
        HttpContext http,
        CancellationToken ct)
    {
        string resolvedCulture = ResolveCulture(http);

        if (deck is null || deck.Cards is null || deck.Cards.Count == 0)
            return Results.Ok(new List<CardOutputDTO>());

        // Collect target collection codes to limit DB query size
        HashSet<string> targetCodes = deck.Cards
            .Where(x => !string.IsNullOrWhiteSpace(x.CollectionCode))
            .Select(x => x.CollectionCode)
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        // Fetch candidate cards with necessary includes
        List<Card> candidates = await db.Cards
            .AsNoTracking()
            .AsSplitQuery()
            .WithAllIncludes()
            .Where(c => targetCodes.Contains(c.Collection.Code)).Include(card => card.Collection)
            .ToListAsync(ct);

        // Index fetched cards by (CollectionCode, CollectionNumber)
        var byPair = candidates
            .GroupBy(c => (Code: c.Collection.Code.ToLowerInvariant(), Num: c.CollectionNumber))
            .ToDictionary(g => g.Key, g => g.First());

        // Build DTOs in the same order as requested, allowing duplicates if present in request
        List<CardOutputDTO> result = new();
        foreach (var req in deck.Cards)
        {
            var key = (Code: (req.CollectionCode ?? string.Empty).ToLowerInvariant(), Num: req.CollectionNumber);
            if (byPair.TryGetValue(key, out var card))
            {
                result.Add(card.ToOutputDTOWithCulture(resolvedCulture));
            }
        }

        return Results.Ok(result);
    }

    #endregion

    #region Methods

    private static string ResolveCulture(HttpContext http)
    {
        if (http.Request.Query.TryGetValue("lng", out StringValues lngVals))
        {
            string culture = lngVals.ToString();
            
            if (string.IsNullOrWhiteSpace(culture)) 
                return "en";
            
            string trimmed = culture.Trim();
            return trimmed.Length >= 2 ? trimmed[..2].ToLowerInvariant() : trimmed.ToLowerInvariant();
        }
        
        string accept = http.Request.Headers.AcceptLanguage.ToString();
        
        if (!string.IsNullOrWhiteSpace(accept) && accept.Trim().StartsWith("fr", StringComparison.OrdinalIgnoreCase))
            return "fr";
        
        return "en";
    }
    
    private static IQueryable<Card> WithAllIncludes(this IQueryable<Card> query)
    {
        return query
            .Include(c => c.Type).ThenInclude(t => t.Translations)
            .Include(c => c.Rarity).ThenInclude(r => r.Translations)
            .Include(c => c.Collection).ThenInclude(s => s.Translations)
            .Include(c => c.Specials).ThenInclude(s => s.Translations)
            .Include(c => c.Translations);
    }

    #endregion
}
