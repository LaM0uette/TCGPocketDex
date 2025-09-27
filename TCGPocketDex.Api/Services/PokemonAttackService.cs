using TCGPocketDex.Api.Repositories;
using TCGPocketDex.Contracts.References;

namespace TCGPocketDex.Api.Services;

public class PokemonAttackService(IPokemonAttackRepository repo) : IPokemonAttackService
{
    public Task<IReadOnlyList<PokemonAttackOutputDTO>> GetAllAsync(string culture, CancellationToken ct) => repo.GetAllAsync(culture, ct);

    public Task<PokemonAttackOutputDTO> CreateAsync(PokemonAttackInputDTO input, CancellationToken ct) => repo.CreateAsync(input, ct);
}
