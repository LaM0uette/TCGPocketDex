using System.ComponentModel.DataAnnotations;

namespace TCGPocketDex.Api.Entities;

public class CardCollectionTranslation
{
    public int Id { get; init; }

    public int CardCollectionId { get; set; }
    public CardCollection? Collection { get; set; }

    [MaxLength(10)]
    public required string Culture { get; set; }

    [MaxLength(100)]
    public required string Name { get; set; }
}