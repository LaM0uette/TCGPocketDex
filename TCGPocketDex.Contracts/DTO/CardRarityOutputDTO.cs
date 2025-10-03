using System.Text.Json.Serialization;

namespace TCGPocketDex.Contracts.DTO;

public class CardRarityOutputDTO
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("name")]
    public required string Name { get; set; }
}
