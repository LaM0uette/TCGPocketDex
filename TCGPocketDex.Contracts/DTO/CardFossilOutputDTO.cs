using System.Text.Json.Serialization;

namespace TCGPocketDex.Contracts.DTO;

public class CardFossilOutputDTO : CardOutputDTO
{
    [JsonPropertyName("hp")]
    public int Hp { get; set; }
}