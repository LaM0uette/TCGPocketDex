using TCGPocketDex.Api.Old.Repositories;
using TCGPocketDex.Contracts.References;

namespace TCGPocketDex.Api.Old.Services;

public class CardRarityService(ICardRarityRepository repo) : ICardRarityService
{
    public Task<IReadOnlyList<CardRarityOutputDTO>> GetAllAsync(CancellationToken ct) => repo.GetAllAsync(ct);

    public Task<CardRarityOutputDTO> CreateAsync(CardRarityInputDTO input, CancellationToken ct) => repo.CreateAsync(input, ct);
}
