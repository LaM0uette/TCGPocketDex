namespace TCGPocketDex.Contracts.DTO;

public record CardPokemonOutputDTO(
    // int Id,
    // CardTypeOutputDTO Type,
    string Name,
    // string? Description,
    string? ImageUrl,
    // List<CardSpecialOutputDTO> Specials,
    // CardRarityOutputDTO Rarity,
    CardCollectionOutputDTO Collection,
    int CollectionNumber
    // List<PokemonSpecialOutputDTO> PokemonSpecials,
    // PokemonStageOutputDTO Stage,
    // int Hp,
    // PokemonTypeOutputDTO PokemonType,
    // PokemonTypeOutputDTO? Weakness,
    // int RetreatCost,
    // PokemonAbilityOutputDTO? Ability,
    // List<PokemonAttackOutputDTO> Attacks
) : CardOutputDTO(
    // Id,
    // Type,
    Name,
    // Description,
    ImageUrl,
    // Specials,
    // Rarity,
    Collection,
    CollectionNumber
);

