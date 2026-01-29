using System.ComponentModel.DataAnnotations;

namespace TCGPocketDex.Api.Entities;

public class Card
{
    public int Id { get; init; }

    public int CardTypeId { get; set; }
    public CardType? Type { get; set; }
    
    [MaxLength(100)]
    public required string Name { get; set; }
    
    [MaxLength(4000)]
    public string? Description { get; set; }
    
    public ICollection<CardSpecial> Specials { get; set; } = [];
    
    public int CardRarityId { get; set; }
    public CardRarity? Rarity { get; set; }

    public int? CardCollectionId { get; set; }
    public CardCollection? Collection { get; set; }
    
    public int CollectionNumber { get; set; }
    
    public CardPokemon? Pokemon { get; set; }
    public CardTool? Tool { get; set; }
    public CardSupporter? Supporter { get; set; }
    public CardFossil? Fossil { get; set; }
    public CardItem? Item { get; set; }
    public CardStadium? Stadium { get; set; }

    public ICollection<CardTranslation> Translations { get; set; } = [];
}