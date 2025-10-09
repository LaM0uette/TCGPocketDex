namespace TCGPocketDex.Contracts.DTO;

public record CardFossilInputDTO(
    string Name,
    string? Description,
    List<int> CardSpecialIds,
    int CardRarityId,
    int CardCollectionId,
    int CollectionNumber,
    int Hp
) : CardInputDTO(
    Name,
    Description,
    CardSpecialIds,
    CardRarityId,
    CardCollectionId,
    CollectionNumber
);