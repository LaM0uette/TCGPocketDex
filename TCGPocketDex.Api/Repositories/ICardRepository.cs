using TCGPocketDex.Api.Entities;

namespace TCGPocketDex.Api.Repositories;

public interface ICardRepository
{
    Task<Card> AddCardAsync(Card card, CancellationToken ct = default);
    Task<CardPokemon> AddPokemonAsync(CardPokemon pokemon, CancellationToken ct = default);
    Task<CardFossil> AddFossilAsync(CardFossil fossil, CancellationToken ct = default);
    Task<CardTool> AddToolAsync(CardTool tool, CancellationToken ct = default);
    Task<CardItem> AddItemAsync(CardItem item, CancellationToken ct = default);
    Task<CardSupporter> AddSupporterAsync(CardSupporter supporter, CancellationToken ct = default);
    Task<CardTranslation> AddCardTranslationAsync(CardTranslation translation, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);

    Task<PokemonType?> FindPokemonTypeAsync(int id, CancellationToken ct = default);
    Task<PokemonStage?> FindPokemonStageAsync(int id, CancellationToken ct = default);
    Task<PokemonAbility?> FindPokemonAbilityAsync(int id, CancellationToken ct = default);
    Task<CardRarity?> FindRarityAsync(int id, CancellationToken ct = default);
    Task<CardCollection?> FindSetAsync(int id, CancellationToken ct = default);
    Task<Card?> FindCardAsync(int id, CancellationToken ct = default);

    Task<CardType?> FindCardTypeAsync(int id, CancellationToken ct = default);
    Task<List<CardSpecial>> FindCardSpecialsByIdsAsync(IEnumerable<int> ids, CancellationToken ct = default);
    Task<List<PokemonSpecial>> FindPokemonSpecialsByIdsAsync(IEnumerable<int> ids, CancellationToken ct = default);

    Task<CardSpecial?> FindCardSpecialByNameAsync(string name, CancellationToken ct = default);
    Task<CardType?> FindCardTypeByNameAsync(string name, CancellationToken ct = default);
}
