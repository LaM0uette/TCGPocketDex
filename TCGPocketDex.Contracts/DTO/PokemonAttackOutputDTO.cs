namespace TCGPocketDex.Contracts.DTO;

public record PokemonAttackOutputDTO(
    int Id,
    int Damage,
    string Name,
    string? Description,
    List<PokemonTypeOutputDTO> Costs
);