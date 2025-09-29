namespace TCGPocketDex.Api.Old.Entity;

public class PokemonWeakness
{
    public int Id { get; init; }

    public int PokemonCardId { get; set; }
    public required PokemonCard Pokemon { get; set; }
    
    public int TypeId { get; set; }
    public required PokemonType Type { get; set; }
}