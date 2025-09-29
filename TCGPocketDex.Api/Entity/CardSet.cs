using System.ComponentModel.DataAnnotations;

namespace TCGPocketDex.Api.Entity;

public class CardSet
{
    public int Id { get; init; }

    [MaxLength(10)]
    public required string Code { get; set; }

    [MaxLength(10)]
    public required string Series { get; set; }
    
    [MaxLength(100)]
    public required string Name { get; set; }

    public ICollection<CardSetTranslation> Translations { get; set; } = [];

    public ICollection<Card> Cards { get; init; } = [];
}