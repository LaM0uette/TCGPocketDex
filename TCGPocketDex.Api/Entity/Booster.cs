namespace TCGPocketDex.Api.Entity;

public class Booster
{
    public int Id { get; init; }

    public int CardExtensionId { get; set; }
    public required CardExtension CardExtension { get; set; }

    public ICollection<BoosterTranslation> Translations { get; init; } = [];

    public ICollection<Card> Cards { get; init; } = [];
}
