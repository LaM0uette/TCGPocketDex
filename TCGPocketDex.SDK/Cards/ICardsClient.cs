using TCGPocketDex.Contracts.Cards;

namespace TCGPocketDex.SDK.Cards;

public interface ICardsClient
{
    Task<IReadOnlyList<CardOutputDTO>> GetAllAsync(string culture, CancellationToken ct = default);
    Task<CardOutputDTO?> GetByIdAsync(int id, string culture, CancellationToken ct = default);
    Task<CardOutputDTO> CreateAsync(CardInputDTO input, CancellationToken ct = default);
    Task<CardOutputDTO?> UpdateAsync(int id, CardInputDTO input, CancellationToken ct = default);
    Task<bool> DeleteAsync(int id, CancellationToken ct = default);
    Task<CardOutputDTO?> AddTranslationAsync(int id, CardTranslationInputDTO input, CancellationToken ct = default);
}
