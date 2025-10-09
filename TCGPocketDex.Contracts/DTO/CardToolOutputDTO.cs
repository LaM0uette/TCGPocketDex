namespace TCGPocketDex.Contracts.DTO;

public record CardToolOutputDTO(
    int Id,
    CardTypeOutputDTO Type,
    string Name,
    string? Description,
    string? ImageUrl,
    List<CardSpecialOutputDTO> Specials,
    CardRarityOutputDTO Rarity,
    CardCollectionOutputDTO Collection,
    int CollectionNumber
) : CardOutputDTO(
    Id,
    Type,
    Name,
    Description,
    ImageUrl,
    Specials,
    Rarity,
    Collection,
    CollectionNumber
);