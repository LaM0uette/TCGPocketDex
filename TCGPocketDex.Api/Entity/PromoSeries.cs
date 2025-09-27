using System.ComponentModel.DataAnnotations;

namespace TCGPocketDex.Api.Entity;

public class PromoSeries
{
    public int Id { get; init; }

    [MaxLength(20)]
    public required string Code { get; set; }

    public ICollection<PromoSeriesTranslation> Translations { get; init; } = [];

    public ICollection<Card> Cards { get; init; } = [];
}
