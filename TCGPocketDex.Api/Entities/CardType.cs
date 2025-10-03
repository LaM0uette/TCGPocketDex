namespace TCGPocketDex.Api.Entities;

public class CardType
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public ICollection<CardTypeTranslation> Translations { get; set; } = [];
}
