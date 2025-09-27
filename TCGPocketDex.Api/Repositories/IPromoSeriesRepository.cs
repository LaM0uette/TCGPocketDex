using TCGPocketDex.Contracts.References;

namespace TCGPocketDex.Api.Repositories;

public interface IPromoSeriesRepository
{
    Task<IReadOnlyList<PromoSeriesOutputDTO>> GetAllAsync(string culture, CancellationToken ct);
    Task<PromoSeriesOutputDTO> CreateAsync(PromoSeriesInputDTO input, CancellationToken ct);
}
