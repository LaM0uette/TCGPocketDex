using System.ComponentModel.DataAnnotations;

namespace TCGPocketDex.Api.Old.Entity;

public class CardExtension
{
    public int Id { get; init; }

    [MaxLength(10)]
    public required string Code { get; set; }

    [MaxLength(10)]
    public required string Series { get; set; }

    public ICollection<CardExtensionTranslation> Translations { get; init; } = [];

    public ICollection<Card> Cards { get; init; } = [];
}