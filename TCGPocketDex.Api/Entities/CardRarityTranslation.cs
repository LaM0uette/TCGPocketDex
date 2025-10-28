using System.ComponentModel.DataAnnotations;

namespace TCGPocketDex.Api.Entities;

public class CardRarityTranslation
{
    public int Id { get; init; }

    public int CardRarityId { get; set; }
    public CardRarity? CardRarity { get; set; }

    [MaxLength(10)]
    public required string Culture { get; set; }

    [MaxLength(50)]
    public required string Name { get; set; }
}
