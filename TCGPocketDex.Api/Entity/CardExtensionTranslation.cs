using System.ComponentModel.DataAnnotations;

namespace TCGPocketDex.Api.Entity;

public class CardExtensionTranslation
{
    public int Id { get; init; }

    public int CardExtensionId { get; set; }
    public required CardExtension CardExtension { get; set; }

    [MaxLength(10)]
    public required string Culture { get; set; }

    [MaxLength(100)]
    public required string Name { get; set; }
}