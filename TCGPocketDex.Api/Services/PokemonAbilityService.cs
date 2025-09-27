using TCGPocketDex.Api.Repositories;
using TCGPocketDex.Contracts.References;

namespace TCGPocketDex.Api.Services;

public class PokemonAbilityService(IPokemonAbilityRepository repo) : IPokemonAbilityService
{
    public Task<IReadOnlyList<PokemonAbilityOutputDTO>> GetAllAsync(string culture, CancellationToken ct) => repo.GetAllAsync(culture, ct);

    public Task<PokemonAbilityOutputDTO> CreateAsync(PokemonAbilityInputDTO input, CancellationToken ct) => repo.CreateAsync(input, ct);
}
