using System.ComponentModel.DataAnnotations;

namespace TCGPocketDex.Api.Old.Entity;

public class BoosterTranslation
{
    public int Id { get; init; }

    public int BoosterId { get; set; }
    public required Booster Booster { get; set; }

    [MaxLength(10)]
    public required string Culture { get; set; }

    [MaxLength(100)]
    public required string Name { get; set; }

    [MaxLength(300)]
    public string? ImageUrl { get; set; }
}
