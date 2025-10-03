using System.ComponentModel.DataAnnotations;

namespace TCGPocketDex.Api.Entities;

public class CardSpecial
{
    public int Id { get; set; }
    
    [MaxLength(100)]
    public required string Name { get; set; }

    public ICollection<CardSpecialTranslation> Translations { get; set; } = [];
}
