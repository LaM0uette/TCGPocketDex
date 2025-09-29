namespace TCGPocketDex.Api.Entity;

public class CardPokemon
{
    public int CardId { get; set; }
    public required Card Card { get; set; }

    public bool IsEX { get; set; }
    public bool IsMega { get; set; }

    public int StageId { get; set; }
    public required PokemonStage Stage { get; set; }

    public int Hp { get; set; }

    public int TypeId { get; set; }
    public required PokemonType Type { get; set; }

    public int? WeaknessTypeId { get; set; }
    public PokemonType? Weakness { get; set; }

    public int RetreatCost { get; set; }
    
    public int? PokemonAbilityId { get; set; }
    public PokemonAbility? Ability { get; set; }

    public ICollection<PokemonAttack> Attacks { get; set; } = [];
}