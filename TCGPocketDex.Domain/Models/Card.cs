namespace TCGPocketDex.Domain.Models;

public record Card(
    int Id,
    CardType Type,
    string Name,
    string Description,
    ICollection<CardSpecial> Specials,
    CardRarity Rarity,
    CardCollection Collection,
    int CollectionNumber
);