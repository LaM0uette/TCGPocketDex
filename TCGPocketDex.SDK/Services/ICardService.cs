using TCGPocketDex.Domain.Models;

namespace TCGPocketDex.SDK.Services;

public interface ICardService
{
    Task<List<Card>> GetAllAsync(CancellationToken ct = default);
    Task<Card?> GetByIdAsync(int id, CancellationToken ct = default);
}