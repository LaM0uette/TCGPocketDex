namespace TCGPocketDex.Contracts.DTO;

public record CardInputDTO(
    string Name,
    string? Description,
    List<int> CardSpecialIds,
    int CardRarityId,
    int CardCollectionId,
    int CollectionNumber
);