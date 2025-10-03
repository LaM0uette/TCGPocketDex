using System.Text.Json.Serialization;

namespace TCGPocketDex.Contracts.DTO;

public class PokemonAbilityOutputDTO
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    
    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;
}
