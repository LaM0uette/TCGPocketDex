using TCGPocketDex.Contracts.References;

namespace TCGPocketDex.Api.Old.Repositories;

public interface IPokemonAttackRepository
{
    Task<IReadOnlyList<PokemonAttackOutputDTO>> GetAllAsync(string culture, CancellationToken ct);
    Task<PokemonAttackOutputDTO> CreateAsync(PokemonAttackInputDTO input, CancellationToken ct);
}
