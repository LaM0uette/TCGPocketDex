namespace TCGPocketDex.Contracts.DTO;

public record CardFossilOutputDTO(
    // int Id,
    CardTypeOutputDTO Type,
    string Name,
    // string? Description,
    string? ImageUrl,
    // List<CardSpecialOutputDTO> Specials,
    // CardRarityOutputDTO Rarity,
    CardCollectionOutputDTO Collection,
    int CollectionNumber
    // int Hp
) : CardOutputDTO(
    // Id,
    Type,
    Name,
    // Description,
    ImageUrl,
    // Specials,
    // Rarity,
    Collection,
    CollectionNumber
);