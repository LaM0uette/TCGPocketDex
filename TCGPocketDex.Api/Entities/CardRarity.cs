using System.ComponentModel.DataAnnotations;

namespace TCGPocketDex.Api.Entities;

public class CardRarity
{
    public int Id { get; init; }

    [MaxLength(50)]
    public required string Name { get; set; }
}