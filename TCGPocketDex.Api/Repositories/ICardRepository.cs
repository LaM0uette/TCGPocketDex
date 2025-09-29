using TCGPocketDex.Api.Entity;

namespace TCGPocketDex.Api.Repositories;

public interface ICardRepository
{
    Task<Card> AddCardAsync(Card card, CancellationToken ct = default);
    Task<CardPokemon> AddPokemonAsync(CardPokemon pokemon, CancellationToken ct = default);
    Task<CardFossil> AddFossilAsync(CardFossil fossil, CancellationToken ct = default);
    Task<CardTool> AddToolAsync(CardTool tool, CancellationToken ct = default);
    Task<CardItem> AddItemAsync(CardItem item, CancellationToken ct = default);
    Task<CardSupporter> AddSupporterAsync(CardSupporter supporter, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);

    Task<PokemonType?> FindPokemonTypeAsync(int id, CancellationToken ct = default);
    Task<PokemonAbility?> FindPokemonAbilityAsync(int id, CancellationToken ct = default);
    Task<CardRarity?> FindRarityAsync(int id, CancellationToken ct = default);
    Task<CardSet?> FindSetAsync(int id, CancellationToken ct = default);
}
