using TCGPocketDex.Contracts.References;

namespace TCGPocketDex.SDK.References;

public interface ICardExtensionsClient
{
    Task<IReadOnlyList<CardExtensionOutputDTO>> GetAllAsync(string culture, CancellationToken ct = default);
    Task<CardExtensionOutputDTO?> CreateAsync(CardExtensionInputDTO dto, CancellationToken ct = default);
}
