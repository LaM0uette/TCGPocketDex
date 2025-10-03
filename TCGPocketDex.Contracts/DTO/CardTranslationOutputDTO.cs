namespace TCGPocketDex.Contracts.DTO;

public class CardTranslationOutputDTO
{
    public int Id { get; set; }
    public int CardId { get; set; }
    public required string Culture { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
}