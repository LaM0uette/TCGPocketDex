using System.Text.Json.Serialization;

namespace TCGPocketDex.Contracts.DTO;

public class CardTypeOutputDTO
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("name")]
    public required string Name { get; set; }
}
