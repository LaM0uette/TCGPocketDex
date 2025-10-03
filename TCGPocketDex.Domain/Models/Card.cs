namespace TCGPocketDex.Domain.Models;

public record Card(
    int Id,
    int CardTypeId,
    string Name,
    string Description,
    ICollection<CardSpecial> Specials,
    CardRarity Rarity,
    int CardSetId,
    int SerieNumber,
    ICollection<CardTranslation> Translations
);