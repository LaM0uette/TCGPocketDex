namespace TCGPocketDex.Contracts.DTO;

public class PokemonAttackOutputDTO
{
    public int Id { get; set; }
    public int Damage { get; set; }
    public required string Name { get; set; }
    public string Description { get; set; } = string.Empty;

    public List<int> Costs { get; set; } = [];
}