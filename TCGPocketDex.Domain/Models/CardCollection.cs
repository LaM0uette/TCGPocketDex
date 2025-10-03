namespace TCGPocketDex.Domain.Models;

public record CardCollection(
    int Id,
    string Code,
    string Series,
    string Name,
    ICollection<CardCollectionTranslation> Translations,
    ICollection<Card> Cards
);