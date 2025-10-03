using System.ComponentModel.DataAnnotations;

namespace TCGPocketDex.Api.Entities;

public class PokemonSpecial
{
    public int Id { get; set; }

    [MaxLength(30)]
    public required string Name { get; set; }

    public ICollection<PokemonSpecialTranslation> Translations { get; set; } = [];
}
