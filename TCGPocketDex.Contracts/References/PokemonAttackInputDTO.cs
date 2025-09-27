namespace TCGPocketDex.Contracts.References;

public record PokemonAttackInputDTO(
    string Culture,
    string Name,
    string? Description,
    int Damage,
    IReadOnlyList<int> CostTypeIds
);
