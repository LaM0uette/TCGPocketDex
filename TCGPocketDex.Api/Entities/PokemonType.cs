using System.ComponentModel.DataAnnotations;

namespace TCGPocketDex.Api.Entities;

public class PokemonType
{
    public int Id { get; init; }
    
    [MaxLength(30)]
    public required string Name { get; set; }

    public ICollection<PokemonTypeTranslation> Translations { get; set; } = [];
}