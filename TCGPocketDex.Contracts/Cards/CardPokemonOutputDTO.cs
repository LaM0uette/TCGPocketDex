namespace TCGPocketDex.Contracts.Cards;

public class CardPokemonOutputDTO
{
    public int Id { get; set; }

    // Common card fields
    public required string Name { get; set; }
    public string? Description { get; set; }
    public bool IsPromo { get; set; }
    public required int CardRarityId { get; set; }
    public int? CardSetId { get; set; }
    public int? SerieNumber { get; set; }

    // Pokemon specific fields
    public int Specials { get; set; } // PokemonSpecial flags
    public int Stage { get; set; }    // PokemonStage

    public int Hp { get; set; }

    public required int TypeId { get; set; }
    public int? WeaknessTypeId { get; set; }

    public int RetreatCost { get; set; }

    public int? PokemonAbilityId { get; set; }

    public List<PokemonAttackOutputDTO> Attacks { get; set; } = [];
}

public class PokemonAttackOutputDTO
{
    public int Id { get; set; }
    public int Damage { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }

    // Costs are PokemonType Ids
    public List<int> Costs { get; set; } = [];
}