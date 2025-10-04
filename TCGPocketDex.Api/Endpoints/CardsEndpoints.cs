using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
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
            .Include(c => c.Type).ThenInclude(t => t.Translations)
            .Include(c => c.Rarity).ThenInclude(r => r.Translations)
            .Include(c => c.Collection).ThenInclude(s => s.Translations)
            .Include(c => c.Specials).ThenInclude(s => s.Translations)
            .Include(c => c.Translations)
            .ToListAsync(ct);
        
        List<CardOutputDTO> dtos = cards.Select(c => c.ToOutputDTOWithCulture(resolvedCulture)).ToList();
        
        return Results.Ok(dtos);
    }

    private static async Task<IResult> GetCardByIdAsync(int id, ApplicationDbContext db, HttpContext http, CancellationToken ct)
    {
        string resolvedCulture = ResolveCulture(http);

        Card? card = await db.Cards
            .AsNoTracking()
            .Include(c => c.Type).ThenInclude(t => t.Translations)
            .Include(c => c.Rarity).ThenInclude(r => r.Translations)
            .Include(c => c.Collection).ThenInclude(s => s.Translations)
            .Include(c => c.Specials).ThenInclude(s => s.Translations)
            .Include(c => c.Translations)
            .FirstOrDefaultAsync(c => c.Id == id, ct);
        if (card is null)
            return Results.NotFound();
        
        CardOutputDTO dto = card.ToOutputDTOWithCulture(resolvedCulture);
        return Results.Ok(dto);
    }
    
    #endregion

    #region Methods

    private static string ResolveCulture(HttpContext http)
    {
        if (http.Request.Query.TryGetValue("lng", out StringValues lngVals))
        {
            string? lng = NormalizeCulture(lngVals.ToString());
            
            if (!string.IsNullOrEmpty(lng))
                return lng;
        }
        
        string accept = http.Request.Headers.AcceptLanguage.ToString();
        
        if (!string.IsNullOrWhiteSpace(accept) && accept.Trim().StartsWith("fr", StringComparison.OrdinalIgnoreCase))
            return "fr";
        
        return "en";
    }

    private static string? NormalizeCulture(string? culture)
    {
        if (string.IsNullOrWhiteSpace(culture)) 
            return null;
        
        string trimmed = culture.Trim();
        
        return trimmed.Length >= 2 ? trimmed[..2].ToLowerInvariant() : trimmed.ToLowerInvariant();
    }

    #endregion
}
