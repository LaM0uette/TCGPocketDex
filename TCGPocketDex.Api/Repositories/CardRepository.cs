using Microsoft.EntityFrameworkCore;
using TCGPocketDex.Api.Data;
using TCGPocketDex.Api.Entities;

namespace TCGPocketDex.Api.Repositories;

public class CardRepository(ApplicationDbContext db) : ICardRepository
{
    public async Task<Card> AddCardAsync(Card card, CancellationToken ct = default)
    {
        await db.Cards.AddAsync(card, ct);
        return card;
    }

    public async Task<CardPokemon> AddPokemonAsync(CardPokemon pokemon, CancellationToken ct = default)
    {
        await db.CardPokemons.AddAsync(pokemon, ct);
        return pokemon;
    }

    public async Task<CardFossil> AddFossilAsync(CardFossil fossil, CancellationToken ct = default)
    {
        await db.CardFossils.AddAsync(fossil, ct);
        return fossil;
    }

    public async Task<CardTool> AddToolAsync(CardTool tool, CancellationToken ct = default)
    {
        await db.CardTools.AddAsync(tool, ct);
        return tool;
    }

    public async Task<CardItem> AddItemAsync(CardItem item, CancellationToken ct = default)
    {
        await db.CardItems.AddAsync(item, ct);
        return item;
    }

    public async Task<CardSupporter> AddSupporterAsync(CardSupporter supporter, CancellationToken ct = default)
    {
        await db.CardSupporters.AddAsync(supporter, ct);
        return supporter;
    }

    public async Task<CardTranslation> AddCardTranslationAsync(CardTranslation translation, CancellationToken ct = default)
    {
        await db.CardTranslations.AddAsync(translation, ct);
        return translation;
    }

    public Task SaveChangesAsync(CancellationToken ct = default) => db.SaveChangesAsync(ct);

    public Task<PokemonType?> FindPokemonTypeAsync(int id, CancellationToken ct = default) => db.PokemonTypes.AsTracking().FirstOrDefaultAsync(t => t.Id == id, ct);
    public Task<PokemonStage?> FindPokemonStageAsync(int id, CancellationToken ct = default) => db.PokemonStages.FirstOrDefaultAsync(s => s.Id == id, ct);
    public Task<PokemonAbility?> FindPokemonAbilityAsync(int id, CancellationToken ct = default) => db.PokemonAbilities.FirstOrDefaultAsync(a => a.Id == id, ct);
    public Task<CardRarity?> FindRarityAsync(int id, CancellationToken ct = default) => db.CardRarities.FirstOrDefaultAsync(r => r.Id == id, ct);
    public Task<CardCollection?> FindSetAsync(int id, CancellationToken ct = default) => db.CardSets.FirstOrDefaultAsync(s => s.Id == id, ct);
    public Task<Card?> FindCardAsync(int id, CancellationToken ct = default) => db.Cards.FirstOrDefaultAsync(c => c.Id == id, ct);

    public Task<CardType?> FindCardTypeAsync(int id, CancellationToken ct = default) => db.CardTypes.FirstOrDefaultAsync(t => t.Id == id, ct);

    public async Task<List<CardSpecial>> FindCardSpecialsByIdsAsync(IEnumerable<int> ids, CancellationToken ct = default)
        => await db.CardSpecials.AsTracking().Where(s => ids.Contains(s.Id)).ToListAsync(ct);

    public async Task<List<PokemonSpecial>> FindPokemonSpecialsByIdsAsync(IEnumerable<int> ids, CancellationToken ct = default)
        => await db.PokemonSpecials.AsTracking().Where(s => ids.Contains(s.Id)).ToListAsync(ct);

    public Task<CardSpecial?> FindCardSpecialByNameAsync(string name, CancellationToken ct = default)
        => db.CardSpecials.FirstOrDefaultAsync(s => s.Name == name, ct);

    public Task<CardType?> FindCardTypeByNameAsync(string name, CancellationToken ct = default)
        => db.CardTypes.FirstOrDefaultAsync(t => t.Name == name, ct);
}
