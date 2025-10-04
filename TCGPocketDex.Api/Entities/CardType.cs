using System.ComponentModel.DataAnnotations;

namespace TCGPocketDex.Api.Entities;

public class CardType
{
    public int Id { get; set; }
    
    [MaxLength(30)]
    public required string Name { get; set; }

    public ICollection<CardTypeTranslation> Translations { get; set; } = [];
}
