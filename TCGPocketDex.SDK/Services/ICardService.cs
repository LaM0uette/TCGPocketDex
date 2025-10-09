using TCGPocketDex.Contracts.Request;
using TCGPocketDex.Domain.Models;

namespace TCGPocketDex.SDK.Services;

public interface ICardService
{
    Task<List<Card>> GetAllAsync(string? cultureOverride = null, CancellationToken ct = default);
    Task<Card?> GetByIdAsync(int id, string? cultureOverride = null, CancellationToken ct = default);
    Task<Card?> GetCardByRequestAsync(CardRequest request, string? cultureOverride = null, CancellationToken ct = default);
    Task<List<Card>> GetCardsByRequestAsync(CardsRequest cards, string? cultureOverride = null, CancellationToken ct = default);
}