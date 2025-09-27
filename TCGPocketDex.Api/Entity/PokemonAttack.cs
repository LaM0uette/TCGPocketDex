namespace TCGPocketDex.Api.Entity;

public class PokemonAttack
{
    public int Id { get; init; }

    public int Damage { get; set; }

    public ICollection<PokemonAttackTranslation> Translations { get; init; } = [];

    public ICollection<PokemonType> Costs { get; init; } = [];
}