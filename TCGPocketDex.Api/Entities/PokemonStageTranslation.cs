using System.ComponentModel.DataAnnotations;

namespace TCGPocketDex.Api.Entities;

public class PokemonStageTranslation
{
    public int Id { get; init; }

    public int PokemonStageId { get; set; }
    public PokemonStage? PokemonStage { get; set; }

    [MaxLength(10)]
    public required string Culture { get; set; }

    [MaxLength(30)]
    public required string Name { get; set; }
}
