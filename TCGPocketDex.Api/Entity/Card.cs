using System.ComponentModel.DataAnnotations;

namespace TCGPocketDex.Api.Entity;

public class Card
{
    public int Id { get; init; }
    
    public required CardKind Kind { get; set; }
    
    [MaxLength(100)]
    public required string Name { get; set; }
    
    [MaxLength(4000)]
    public string? Description { get; set; }
    
    public bool IsPromo { get; set; }
    
    public int CardRarityId { get; set; }
    public required CardRarity Rarity { get; set; }

    public int? CardSetId { get; set; }
    public CardSet? CardSet { get; set; }

    public int? SerieNumber { get; set; }
    
    public CardPokemon? Pokemon { get; set; }
    public CardTool? Tool { get; set; }
    public CardSupporter? Supporter { get; set; }
    public CardFossil? Fossil { get; set; }
    public CardItem? Item { get; set; }

    public ICollection<CardTranslation> Translations { get; set; } = [];
}