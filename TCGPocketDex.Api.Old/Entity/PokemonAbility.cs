namespace TCGPocketDex.Api.Old.Entity;

public class PokemonAbility
{
    public int Id { get; init; }

    public ICollection<PokemonAbilityTranslation> Translations { get; init; } = [];
}