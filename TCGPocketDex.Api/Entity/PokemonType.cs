namespace TCGPocketDex.Api.Entity;

public class PokemonType
{
    public int Id { get; init; }

    public ICollection<PokemonTypeTranslation> Translations { get; init; } = [];

    public ICollection<PokemonAttack> AttacksUsingType { get; init; } = [];
}