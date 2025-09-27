namespace TCGPocketDex.Api.Entity;

public class PokemonWeakness
{
    public int Id { get; init; }
    
    public required PokemonType Type { get; set; }
}