using TCGPocketDex.Api.Repositories;
using TCGPocketDex.Contracts.References;

namespace TCGPocketDex.Api.Services;

public class CardExtensionService(ICardExtensionRepository repo) : ICardExtensionService
{
    public Task<IReadOnlyList<CardExtensionOutputDTO>> GetAllAsync(string culture, CancellationToken ct) => repo.GetAllAsync(culture, ct);

    public Task<CardExtensionOutputDTO> CreateAsync(CardExtensionInputDTO input, CancellationToken ct) => repo.CreateAsync(input, ct);
}
