using TCGPocketDex.Contracts.References;

namespace TCGPocketDex.Api.Old.Repositories;

public interface IPokemonStageRepository
{
    Task<IReadOnlyList<PokemonStageOutputDTO>> GetAllAsync(string culture, CancellationToken ct);
    Task<PokemonStageOutputDTO> CreateAsync(PokemonStageInputDTO input, CancellationToken ct);
}
