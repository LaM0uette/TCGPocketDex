namespace TCGPocketDex.Api.Old.Entity;

public class PokemonCard : Card
{
    public bool IsEx { get; set; }
    public bool IsMega { get; set; }

    public int StageId { get; set; }
    public required PokemonStage Stage { get; set; }

    public int Hp { get; set; }

    public int TypeId { get; set; }
    public required PokemonType Type { get; set; }

    public PokemonWeakness? Weakness { get; set; }

    public int RetreatCost { get; set; }

    public int? PokemonAbilityId { get; set; }
    public PokemonAbility? Ability { get; set; }

    public ICollection<PokemonAttack> Attacks { get; init; } = [];
}