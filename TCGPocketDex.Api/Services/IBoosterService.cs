using TCGPocketDex.Contracts.References;

namespace TCGPocketDex.Api.Services;

public interface IBoosterService
{
    Task<IReadOnlyList<BoosterOutputDTO>> GetAllAsync(string culture, int? cardExtensionId, CancellationToken ct);
    Task<BoosterOutputDTO> CreateAsync(BoosterInputDTO input, CancellationToken ct);
}
