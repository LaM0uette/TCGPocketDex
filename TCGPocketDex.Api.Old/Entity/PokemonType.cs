namespace TCGPocketDex.Api.Old.Entity;

public class PokemonType
{
    public int Id { get; init; }

    public ICollection<PokemonTypeTranslation> Translations { get; init; } = [];

    public ICollection<PokemonAttack> AttacksUsingType { get; init; } = [];
}