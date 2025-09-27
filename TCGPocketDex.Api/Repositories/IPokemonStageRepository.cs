using TCGPocketDex.Contracts.References;

namespace TCGPocketDex.Api.Repositories;

public interface IPokemonStageRepository
{
    Task<IReadOnlyList<PokemonStageOutputDTO>> GetAllAsync(string culture, CancellationToken ct);
    Task<PokemonStageOutputDTO> CreateAsync(PokemonStageInputDTO input, CancellationToken ct);
}
