namespace TCGPocketDex.Contracts.Cards;

public class CardPokemonInputDTO
{
    // Common card fields
    public required string Name { get; set; }
    public string? Description { get; set; }
    public bool IsPromo { get; set; }
    public required int CardRarityId { get; set; }
    public int? CardSetId { get; set; }
    public int? SerieNumber { get; set; }

    // Pokemon specific fields
    // Use ints for enums to avoid coupling. Values should match API enums.
    public int Specials { get; set; } // PokemonSpecial flags
    public int Stage { get; set; }    // PokemonStage

    public int Hp { get; set; }

    public required int TypeId { get; set; }
    public int? WeaknessTypeId { get; set; }

    public int RetreatCost { get; set; }

    public int? PokemonAbilityId { get; set; }

    public List<PokemonAttackInputDTO> Attacks { get; set; } = [];
}

public class PokemonAttackInputDTO
{
    public int Damage { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }

    // Costs are PokemonType Ids
    public List<int> Costs { get; set; } = [];
}