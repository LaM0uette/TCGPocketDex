using TCGPocketDex.Api.Old.Repositories;
using TCGPocketDex.Contracts.References;

namespace TCGPocketDex.Api.Old.Services;

public class BoosterService(IBoosterRepository repo) : IBoosterService
{
    public Task<IReadOnlyList<BoosterOutputDTO>> GetAllAsync(string culture, int? cardExtensionId, CancellationToken ct) => repo.GetAllAsync(culture, cardExtensionId, ct);

    public Task<BoosterOutputDTO> CreateAsync(BoosterInputDTO input, CancellationToken ct) => repo.CreateAsync(input, ct);
}
