namespace TCGPocketDex.Contracts.DTO;

public class CardTranslationInputDTO
{
    public required string Culture { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
}