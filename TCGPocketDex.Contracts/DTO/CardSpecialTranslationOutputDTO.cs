using System.Text.Json.Serialization;

namespace TCGPocketDex.Contracts.DTO;

public class CardSpecialTranslationOutputDTO
{
    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("culture")]
    public required string Culture { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }
}
