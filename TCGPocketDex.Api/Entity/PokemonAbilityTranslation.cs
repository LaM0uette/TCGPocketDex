using System.ComponentModel.DataAnnotations;

namespace TCGPocketDex.Api.Entity;

public class PokemonAbilityTranslation
{
    public int Id { get; init; }

    public int PokemonAbilityId { get; set; }
    public required PokemonAbility PokemonAbility { get; set; }

    [MaxLength(10)]
    public required string Culture { get; set; }

    [MaxLength(100)]
    public required string Name { get; set; }
}