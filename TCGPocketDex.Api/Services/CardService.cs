using TCGPocketDex.Api.Repositories;
using TCGPocketDex.Contracts.Cards;

namespace TCGPocketDex.Api.Services;

public class CardService(ICardRepository repo) : ICardService
{
    public Task<IReadOnlyList<CardOutputDTO>> GetAllAsync(string culture, CancellationToken ct) => repo.GetAllAsync(culture, ct);
    public Task<CardOutputDTO?> GetByIdAsync(int id, string culture, CancellationToken ct) => repo.GetByIdAsync(id, culture, ct);
    public Task<CardOutputDTO> CreateAsync(CardInputDTO input, CancellationToken ct) => repo.CreateAsync(input, ct);
    public Task<CardOutputDTO?> UpdateAsync(int id, CardInputDTO input, CancellationToken ct) => repo.UpdateAsync(id, input, ct);
    public Task<bool> DeleteAsync(int id, CancellationToken ct) => repo.DeleteAsync(id, ct);
    public Task<CardOutputDTO?> AddTranslationAsync(int id, CardTranslationInputDTO input, CancellationToken ct) => repo.AddTranslationAsync(id, input, ct);
}