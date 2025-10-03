namespace TCGPocketDex.Contracts.DTO;

public class PokemonAttackInputDTO
{
    public int Damage { get; set; }
    public required string Name { get; set; }
    public string Description { get; set; } = string.Empty;

    public List<int> Costs { get; set; } = [];
}