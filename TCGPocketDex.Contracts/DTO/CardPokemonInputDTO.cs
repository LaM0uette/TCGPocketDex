namespace TCGPocketDex.Contracts.DTO;

public record CardPokemonInputDTO(
    string Name,
    string? Description,
    List<int> CardSpecialIds,
    int CardRarityId,
    int CardCollectionId,
    int CollectionNumber,
    List<int> PokemonSpecialIds,
    int PokemonStageId,
    int Hp,
    int PokemonTypeId,
    int? WeaknessPokemonTypeId,
    int RetreatCost,
    int? PokemonAbilityId,
    List<PokemonAttackInputDTO> Attacks
) : CardInputDTO(
    Name,
    Description,
    CardSpecialIds,
    CardRarityId,
    CardCollectionId,
    CollectionNumber
);