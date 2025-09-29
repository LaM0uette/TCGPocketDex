using System.ComponentModel.DataAnnotations;

namespace TCGPocketDex.Api.Entity;

public class PokemonAttack
{
    public int Id { get; init; }

    public int Damage { get; set; }
    
    [MaxLength(100)]
    public required string Name { get; set; }

    [MaxLength(4000)]
    public string? Description { get; set; }

    public ICollection<PokemonAttackTranslation> Translations { get; set; } = [];

    public ICollection<PokemonType> Costs { get; set; } = [];
}