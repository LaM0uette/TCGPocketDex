using TCGPocketDex.Contracts.Cards;

namespace TCGPocketDex.Api.Services;

public interface ICardService
{
    Task<IReadOnlyList<CardOutputDTO>> GetAllAsync(string culture, CancellationToken ct);
    Task<CardOutputDTO?> GetByIdAsync(int id, string culture, CancellationToken ct);
    Task<CardOutputDTO> CreateAsync(CardInputDTO input, CancellationToken ct);
    Task<CardOutputDTO?> UpdateAsync(int id, CardInputDTO input, CancellationToken ct);
    Task<bool> DeleteAsync(int id, CancellationToken ct);
    Task<CardOutputDTO?> AddTranslationAsync(int id, CardTranslationInputDTO input, CancellationToken ct);
}