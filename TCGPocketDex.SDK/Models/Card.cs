namespace TCGPocketDex.SDK.Models;

public class Card
{
    public int Id { get; set; }
    public int CardTypeId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public List<int> CardSpecialIds { get; set; } = [];
    public int CardRarityId { get; set; }
    public int CardSetId { get; set; }
    public int SerieNumber { get; set; }
}