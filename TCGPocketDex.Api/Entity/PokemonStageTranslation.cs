using System.ComponentModel.DataAnnotations;

namespace TCGPocketDex.Api.Entity;

public class PokemonStageTranslation
{
    public int Id { get; init; }

    public PokemonStage Stage { get; set; }

    [MaxLength(10)]
    public required string Culture { get; set; }

    [MaxLength(30)]
    public required string Name { get; set; }
}