using TCGPocketDex.Api.Repositories;
using TCGPocketDex.Contracts.References;

namespace TCGPocketDex.Api.Services;

public class PromoSeriesService(IPromoSeriesRepository repo) : IPromoSeriesService
{
    public Task<IReadOnlyList<PromoSeriesOutputDTO>> GetAllAsync(string culture, CancellationToken ct) => repo.GetAllAsync(culture, ct);

    public Task<PromoSeriesOutputDTO> CreateAsync(PromoSeriesInputDTO input, CancellationToken ct) => repo.CreateAsync(input, ct);
}
