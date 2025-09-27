using TCGPocketDex.Contracts.References;

namespace TCGPocketDex.Api.Services;

public interface ICardExtensionService
{
    Task<IReadOnlyList<CardExtensionOutputDTO>> GetAllAsync(string culture, CancellationToken ct);
    Task<CardExtensionOutputDTO> CreateAsync(CardExtensionInputDTO input, CancellationToken ct);
}
