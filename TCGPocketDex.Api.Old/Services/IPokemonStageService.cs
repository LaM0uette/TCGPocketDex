using TCGPocketDex.Contracts.References;

namespace TCGPocketDex.Api.Old.Services;

public interface IPokemonStageService
{
    Task<IReadOnlyList<PokemonStageOutputDTO>> GetAllAsync(string culture, CancellationToken ct);
    Task<PokemonStageOutputDTO> CreateAsync(PokemonStageInputDTO input, CancellationToken ct);
}
