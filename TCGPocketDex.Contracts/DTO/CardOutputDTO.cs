namespace TCGPocketDex.Contracts.DTO;

public record CardOutputDTO(
    // int Id,
    // CardTypeOutputDTO Type,
    string Name,
    // string? Description,
    string? ImageUrl,
    // List<CardSpecialOutputDTO> Specials,
    // CardRarityOutputDTO Rarity,
    CardCollectionOutputDTO Collection,
    int CollectionNumber
);