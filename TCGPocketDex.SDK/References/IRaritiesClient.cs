using TCGPocketDex.Contracts.References;

namespace TCGPocketDex.SDK.References;

public interface IRaritiesClient
{
    Task<IReadOnlyList<CardRarityOutputDTO>> GetAllAsync(CancellationToken ct = default);
    Task<CardRarityOutputDTO?> CreateAsync(CardRarityInputDTO dto, CancellationToken ct = default);
}
