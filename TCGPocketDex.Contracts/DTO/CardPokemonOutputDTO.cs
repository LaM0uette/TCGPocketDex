using System.Text.Json.Serialization;

namespace TCGPocketDex.Contracts.DTO;

public class CardPokemonOutputDTO : CardOutputDTO
{
    [JsonPropertyName("specials")]
    public List<PokemonSpecialOutputDTO> PokemonSpecials { get; set; } = [];
    
    [JsonPropertyName("stage")]
    public required PokemonStageOutputDTO Stage { get; set; }

    [JsonPropertyName("hp")]
    public int Hp { get; set; }

    [JsonPropertyName("type")]
    public required PokemonTypeOutputDTO PokemonType { get; set; }
    
    [JsonPropertyName("weakness")]
    public PokemonTypeOutputDTO? Weakness { get; set; }

    [JsonPropertyName("retreatCost")]
    public int RetreatCost { get; set; }

    [JsonPropertyName("ability")]
    public PokemonAbilityOutputDTO? Ability { get; set; }

    [JsonPropertyName("attacks")]
    public List<PokemonAttackOutputDTO> Attacks { get; set; } = [];
}

