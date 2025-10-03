namespace TCGPocketDex.Contracts.DTO;

public class CardItemOutputDTO
{
    public int Id { get; set; }

    // Common card fields
    public required string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public List<int> CardSpecialIds { get; set; } = [];
    public required int CardRarityId { get; set; }
    public int? CardSetId { get; set; }
    public int? SerieNumber { get; set; }
}