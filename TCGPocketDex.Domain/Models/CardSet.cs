namespace TCGPocketDex.Domain.Models;

public record CardSet(
    int Id,
    string Code,
    string Series,
    string Name,
    ICollection<CardSetTranslation> Translations,
    ICollection<Card> Cards
);