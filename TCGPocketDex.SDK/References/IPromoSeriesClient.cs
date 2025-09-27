using TCGPocketDex.Contracts.References;

namespace TCGPocketDex.SDK.References;

public interface IPromoSeriesClient
{
    Task<IReadOnlyList<PromoSeriesOutputDTO>> GetAllAsync(string culture, CancellationToken ct = default);
    Task<PromoSeriesOutputDTO?> CreateAsync(PromoSeriesInputDTO dto, CancellationToken ct = default);
}
