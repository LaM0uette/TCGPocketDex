namespace TCGPocketDex.Contracts.Cards;

public record CardInputDTO(
    string Culture,
    string Name,
    string? Description,
    string ImageUrl,
    int CardRarityId,
    int? BoosterId,
    int? CardExtensionId,
    int? PromoSeriesId,
    CardKind Kind,
    int? FossilHp,
    bool? PokemonIsEx,
    bool? PokemonIsMega,
    int? PokemonStageId,
    int? PokemonHp,
    int? PokemonTypeId,
    int? PokemonWeaknessTypeId,
    int? PokemonRetreatCost,
    int? PokemonAbilityId,
    IReadOnlyList<int>? PokemonAttackIds
);