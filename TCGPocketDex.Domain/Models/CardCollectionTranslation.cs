namespace TCGPocketDex.Domain.Models;

public record CardCollectionTranslation(
    int Id,
    int CollectionId,
    string Culture,
    string Name
);