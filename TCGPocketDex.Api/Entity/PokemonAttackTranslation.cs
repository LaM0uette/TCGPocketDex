using System.ComponentModel.DataAnnotations;

namespace TCGPocketDex.Api.Entity;

public class PokemonAttackTranslation
{
    public int Id { get; init; }

    public int PokemonAttackId { get; set; }
    public required PokemonAttack PokemonAttack { get; set; }

    [MaxLength(10)]
    public required string Culture { get; set; }

    [MaxLength(100)]
    public required string Name { get; set; }

    [MaxLength(2000)]
    public string? Description { get; set; }
}