using System.ComponentModel.DataAnnotations;

namespace TCGPocketDex.Api.Entity;

public class PokemonType
{
    public int Id { get; init; }
    
    [MaxLength(30)]
    public required string Name { get; set; }

    public ICollection<PokemonTypeTranslation> Translations { get; set; } = [];
}