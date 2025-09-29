using System.ComponentModel.DataAnnotations;

namespace TCGPocketDex.Api.Entity;

public class PokemonAbility
{
    public int Id { get; init; }
    
    [MaxLength(100)]
    public required string Name { get; set; }

    public ICollection<PokemonAbilityTranslation> Translations { get; set; } = [];
}