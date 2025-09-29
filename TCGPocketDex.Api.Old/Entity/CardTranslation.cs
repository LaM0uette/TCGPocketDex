using System.ComponentModel.DataAnnotations;

namespace TCGPocketDex.Api.Old.Entity;

public class CardTranslation
{
    public int Id { get; init; }

    public int CardId { get; set; }
    public required Card Card { get; set; }

    [MaxLength(10)]
    public required string Culture { get; set; }

    [MaxLength(100)]
    public required string Name { get; set; }

    [MaxLength(2000)]
    public string? Description { get; set; }

    [MaxLength(255)]
    public required string ImageUrl { get; set; }
}