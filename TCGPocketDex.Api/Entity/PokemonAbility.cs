namespace TCGPocketDex.Api.Entity;

public class PokemonAbility
{
    public int Id { get; init; }

    public ICollection<PokemonAbilityTranslation> Translations { get; init; } = [];
}