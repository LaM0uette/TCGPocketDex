using System.ComponentModel.DataAnnotations;

namespace TCGPocketDex.Api.Entity;

public class PokemonStage
{
    public int Id { get; init; }

    [MaxLength(30)]
    public required string Name { get; set; }
}