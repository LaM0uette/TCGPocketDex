using System.ComponentModel.DataAnnotations;

namespace TCGPocketDex.Api.Entity;

public class CardSetTranslation
{
    public int Id { get; init; }

    public int CardSetId { get; set; }
    public required CardSet CardSet { get; set; }

    [MaxLength(10)]
    public required string Culture { get; set; }

    [MaxLength(100)]
    public required string Name { get; set; }
}