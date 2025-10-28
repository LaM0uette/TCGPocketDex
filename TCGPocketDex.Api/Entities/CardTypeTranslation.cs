using System.ComponentModel.DataAnnotations;

namespace TCGPocketDex.Api.Entities;

public class CardTypeTranslation
{
    public int Id { get; init; }

    public int CardTypeId { get; set; }
    public CardType? CardType { get; set; }

    [MaxLength(10)]
    public required string Culture { get; set; }

    [MaxLength(30)]
    public required string Name { get; set; }
}
