using TCGPocketDex.Contracts.References;

namespace TCGPocketDex.Api.Old.Services;

public interface ICardRarityService
{
    Task<IReadOnlyList<CardRarityOutputDTO>> GetAllAsync(CancellationToken ct);
    Task<CardRarityOutputDTO> CreateAsync(CardRarityInputDTO input, CancellationToken ct);
}
