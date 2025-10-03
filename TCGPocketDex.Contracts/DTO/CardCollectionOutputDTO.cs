using System.Text.Json.Serialization;

namespace TCGPocketDex.Contracts.DTO;

public class CardCollectionOutputDTO
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("code")]
    public required string Code { get; set; }
    
    [JsonPropertyName("series")]
    public required string Series { get; set; }
    
    [JsonPropertyName("name")]
    public required string Name { get; set; }
}
