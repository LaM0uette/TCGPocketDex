using System.ComponentModel.DataAnnotations;

namespace TCGPocketDex.Api.Entities;

public class CardTranslation
{
    public int Id { get; init; }
    
    public int CardId { get; set; }
    public Card? Card { get; set; }
    
    [MaxLength(10)]
    public required string Culture { get; set; }
    
    [MaxLength(100)]
    public required string Name { get; set; }
    
    [MaxLength(4000)]
    public string? Description { get; set; }
}