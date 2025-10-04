namespace TCGPocketDex.Domain.Models;

public record Card(
    int Id,
    CardType Type,
    string Name,
    string Description,
    string ImageUrl,
    ICollection<CardSpecial> Specials,
    CardRarity Rarity,
    CardCollection Collection,
    int CollectionNumber
);