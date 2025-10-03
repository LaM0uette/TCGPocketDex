using System.ComponentModel.DataAnnotations;

namespace TCGPocketDex.Api.Entities;

public class PokemonAttack
{
    public int Id { get; init; }
    
    [MaxLength(100)]
    public required string Name { get; set; }

    [MaxLength(4000)]
    public string? Description { get; set; }
    
    public int Damage { get; set; }

    public ICollection<PokemonAttackTranslation> Translations { get; set; } = [];
    public ICollection<PokemonType> Costs { get; set; } = [];
}