namespace TCGPocketDex.Api.Entities;

public class CardPokemon
{
    public int CardId { get; set; }
    public required Card Card { get; set; }

    public ICollection<PokemonSpecial> Specials { get; set; } = [];

    public int PokemonStageId { get; set; }
    public required PokemonStage Stage { get; set; }

    public int Hp { get; set; }

    public int PokemonTypeId { get; set; }
    public required PokemonType Type { get; set; }

    public int? WeaknessPokemonTypeId { get; set; }
    public PokemonType? Weakness { get; set; }

    public int RetreatCost { get; set; }
    
    public int? PokemonAbilityId { get; set; }
    public PokemonAbility? Ability { get; set; }

    public ICollection<PokemonAttack> Attacks { get; set; } = [];
}