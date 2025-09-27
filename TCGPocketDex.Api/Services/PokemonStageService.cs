using TCGPocketDex.Api.Repositories;
using TCGPocketDex.Contracts.References;

namespace TCGPocketDex.Api.Services;

public class PokemonStageService(IPokemonStageRepository repo) : IPokemonStageService
{
    public Task<IReadOnlyList<PokemonStageOutputDTO>> GetAllAsync(string culture, CancellationToken ct) => repo.GetAllAsync(culture, ct);

    public Task<PokemonStageOutputDTO> CreateAsync(PokemonStageInputDTO input, CancellationToken ct) => repo.CreateAsync(input, ct);
}
