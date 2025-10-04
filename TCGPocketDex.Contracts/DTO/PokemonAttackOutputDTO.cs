using System.Text.Json.Serialization;

namespace TCGPocketDex.Contracts.DTO;

public class PokemonAttackOutputDTO
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("damage")]
    public int Damage { get; set; }
    
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    
    [JsonPropertyName("description")]
    public string? Description { get; set; }
    
    [JsonPropertyName("costs")]
    public List<PokemonTypeOutputDTO> Costs { get; set; } = [];
}