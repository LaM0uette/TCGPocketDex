using TCGPocketDex.Contracts.References;

namespace TCGPocketDex.Api.Old.Services;

public interface ICardExtensionService
{
    Task<IReadOnlyList<CardExtensionOutputDTO>> GetAllAsync(string culture, CancellationToken ct);
    Task<CardExtensionOutputDTO> CreateAsync(CardExtensionInputDTO input, CancellationToken ct);
}
