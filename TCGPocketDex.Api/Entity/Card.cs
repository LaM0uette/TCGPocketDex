namespace TCGPocketDex.Api.Entity;

public class Card
{
    public int Id { get; init; }

    public ICollection<CardTranslation> Translations { get; init; } = [];

    public int CardRarityId { get; set; }
    public required CardRarity Rarity { get; set; }

    public int? BoosterId { get; set; }
    public Booster? Booster { get; set; }

    public int? PromoSeriesId { get; set; }
    public PromoSeries? PromoSeries { get; set; }

    public int? CardExtensionId { get; set; }
    public CardExtension? Extension { get; set; }

    public int? ExtensionCardNumber { get; set; }
}