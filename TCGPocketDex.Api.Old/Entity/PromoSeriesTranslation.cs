using System.ComponentModel.DataAnnotations;

namespace TCGPocketDex.Api.Old.Entity;

public class PromoSeriesTranslation
{
    public int Id { get; init; }

    public int PromoSeriesId { get; set; }
    public required PromoSeries PromoSeries { get; set; }

    [MaxLength(10)]
    public required string Culture { get; set; }

    [MaxLength(100)]
    public required string Name { get; set; }
}
