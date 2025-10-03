using System.Text.Json.Serialization;

namespace TCGPocketDex.Contracts.DTO;

public class CardOutputDTO
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("cardTypeId")]
    public int CardTypeId { get; set; }
    
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    
    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;
    
    [JsonPropertyName("cardSpecialIds")]
    public List<int> CardSpecialIds { get; set; } = [];
    
    [JsonPropertyName("cardRarityId")]
    public int CardRarityId { get; set; }
    
    [JsonPropertyName("cardSetId")]
    public int CardSetId { get; set; }
    
    [JsonPropertyName("serieNumber")]
    public int SerieNumber { get; set; }
}

