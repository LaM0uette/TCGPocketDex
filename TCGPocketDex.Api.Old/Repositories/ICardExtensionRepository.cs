using TCGPocketDex.Contracts.References;

namespace TCGPocketDex.Api.Old.Repositories;

public interface ICardExtensionRepository
{
    Task<IReadOnlyList<CardExtensionOutputDTO>> GetAllAsync(string culture, CancellationToken ct);
    Task<CardExtensionOutputDTO> CreateAsync(CardExtensionInputDTO input, CancellationToken ct);
}
