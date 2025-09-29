namespace TCGPocketDex.Api.Old.Entity;

public class PokemonStage
{
    public int Id { get; init; }

    public ICollection<PokemonStageTranslation> Translations { get; init; } = [];
}