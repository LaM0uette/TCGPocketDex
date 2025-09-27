using TCGPocketDex.Contracts.References;

namespace TCGPocketDex.Api.Repositories;

public interface IBoosterRepository
{
    Task<IReadOnlyList<BoosterOutputDTO>> GetAllAsync(string culture, int? cardExtensionId, CancellationToken ct);
    Task<BoosterOutputDTO> CreateAsync(BoosterInputDTO input, CancellationToken ct);
}
