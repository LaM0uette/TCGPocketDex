using System.ComponentModel.DataAnnotations;

namespace TCGPocketDex.Api.Entity;

public class PokemonTypeTranslation
{
    public int Id { get; init; }

    public int PokemonTypeId { get; set; }
    public required PokemonType PokemonType { get; set; }

    [MaxLength(10)]
    public required string Culture { get; set; }

    [MaxLength(30)]
    public required string Name { get; set; }
}