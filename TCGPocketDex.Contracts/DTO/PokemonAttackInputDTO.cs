namespace TCGPocketDex.Contracts.DTO;

public record PokemonAttackInputDTO(
    int Damage,
    string Name,
    string? Description,
    List<int> Costs
);