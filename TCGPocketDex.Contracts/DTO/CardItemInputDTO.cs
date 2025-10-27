namespace TCGPocketDex.Contracts.DTO;

public record CardItemInputDTO(
    string Name,
    string? Description,
    List<int> CardSpecialIds,
    int CardRarityId,
    int CardCollectionId,
    int CollectionNumber
) : CardInputDTO(
    Name,
    Description,
    CardSpecialIds,
    CardRarityId,
    CardCollectionId,
    CollectionNumber
);