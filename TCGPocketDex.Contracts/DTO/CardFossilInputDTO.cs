namespace TCGPocketDex.Contracts.DTO;

public class CardFossilOutputDTO
{
    public int Id { get; set; }

    // Common card fields
    public required string Name { get; set; }
    public string? Description { get; set; }
    public bool IsPromo { get; set; }
    public required int CardRarityId { get; set; }
    public int? CardSetId { get; set; }
    public int? SerieNumber { get; set; }

    // Fossil specific
    public int Hp { get; set; }
}