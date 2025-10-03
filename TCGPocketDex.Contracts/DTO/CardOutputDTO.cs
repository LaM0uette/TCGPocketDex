using System.Text.Json.Serialization;

namespace TCGPocketDex.Contracts.DTO;

public class CardOutputDTO
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("cardType")]
    public required CardTypeOutputDTO Type { get; set; }
    
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    
    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;
    
    [JsonPropertyName("specials")]
    public List<CardSpecialOutputDTO> Specials { get; set; } = [];
    
    [JsonPropertyName("rarity")]
    public required CardRarityOutputDTO Rarity { get; set; }
    
    [JsonPropertyName("collection")]
    public required CardCollectionOutputDTO Collection { get; set; }
    
    [JsonPropertyName("collectionNumber")]
    public int CollectionNumber { get; set; }
    
    [JsonPropertyName("translations")]
    public List<CardTranslationOutputDTO> Translations { get; set; } = [];
}

