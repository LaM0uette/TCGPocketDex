using System.ComponentModel.DataAnnotations;

namespace TCGPocketDex.Api.Entities;

public class CardSpecialTranslation
{
    public int Id { get; init; }

    public int CardSpecialId { get; set; }
    public CardSpecial? CardSpecial { get; set; }

    [MaxLength(10)]
    public required string Culture { get; set; }

    [MaxLength(100)]
    public required string Name { get; set; }
}
