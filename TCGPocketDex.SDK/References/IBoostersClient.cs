using TCGPocketDex.Contracts.References;

namespace TCGPocketDex.SDK.References;

public interface IBoostersClient
{
    Task<IReadOnlyList<BoosterOutputDTO>> GetAllAsync(string culture, int? cardExtensionId = null, CancellationToken ct = default);
    Task<BoosterOutputDTO?> CreateAsync(BoosterInputDTO dto, CancellationToken ct = default);
}
