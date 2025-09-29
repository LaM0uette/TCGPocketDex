using TCGPocketDex.Contracts.References;

namespace TCGPocketDex.Api.Old.Repositories;

public interface ICardRarityRepository
{
    Task<IReadOnlyList<CardRarityOutputDTO>> GetAllAsync(CancellationToken ct);
    Task<CardRarityOutputDTO> CreateAsync(CardRarityInputDTO input, CancellationToken ct);
}
