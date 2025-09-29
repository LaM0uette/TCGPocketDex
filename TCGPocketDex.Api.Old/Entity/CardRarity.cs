using System.ComponentModel.DataAnnotations;

namespace TCGPocketDex.Api.Old.Entity;

public class CardRarity
{
    public int Id { get; init; }

    [MaxLength(50)]
    public required string Name { get; set; }
    
    [MaxLength(255)]
    public required string ImageUrl { get; set; }
}