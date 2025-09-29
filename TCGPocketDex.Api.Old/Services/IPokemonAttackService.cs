using TCGPocketDex.Contracts.References;

namespace TCGPocketDex.Api.Old.Services;

public interface IPokemonAttackService
{
    Task<IReadOnlyList<PokemonAttackOutputDTO>> GetAllAsync(string culture, CancellationToken ct);
    Task<PokemonAttackOutputDTO> CreateAsync(PokemonAttackInputDTO input, CancellationToken ct);
}
