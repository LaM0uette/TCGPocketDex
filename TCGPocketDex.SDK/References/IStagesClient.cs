using TCGPocketDex.Contracts.References;

namespace TCGPocketDex.SDK.References;

public interface IStagesClient
{
    Task<IReadOnlyList<PokemonStageOutputDTO>> GetAllAsync(string culture, CancellationToken ct = default);
    Task<PokemonStageOutputDTO?> CreateAsync(PokemonStageInputDTO dto, CancellationToken ct = default);
}
