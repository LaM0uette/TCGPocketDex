using System.Text.Json.Serialization;

namespace TCGPocketDex.Contracts.DTO;

public class CardSpecialOutputDTO
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    
    [JsonPropertyName("translation")]
    public List<CardSpecialTranslationOutputDTO> Translation { get; set; } = [];
}
