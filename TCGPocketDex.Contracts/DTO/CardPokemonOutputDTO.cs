namespace TCGPocketDex.Contracts.DTO;

public class CardPokemonOutputDTO
{
    public int Id { get; set; }
    
    public required string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public List<int> CardSpecialIds { get; set; } = [];
    public required int CardRarityId { get; set; }
    public int? CardSetId { get; set; }
    public int? SerieNumber { get; set; }

    // Pokemon specific fields
    public List<int> PokemonSpecialIds { get; set; } = [];
    public int Stage { get; set; }    // PokemonStage

    public int Hp { get; set; }

    public required int TypeId { get; set; }
    public int? WeaknessTypeId { get; set; }

    public int RetreatCost { get; set; }

    public int? PokemonAbilityId { get; set; }

    public List<PokemonAttackOutputDTO> Attacks { get; set; } = [];
}

