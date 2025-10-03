using TCGPocketDex.SDK.Models;

namespace TCGPocketDex.SDK.Services;

public interface ICardService
{
    Task<List<Card>> GetAllAsync(CancellationToken ct = default);
    Task<Card?> GetByIdAsync(int id, CancellationToken ct = default);
    // For Web client: returns DTOs directly from API public endpoint
    Task<List<TCGPocketDex.Contracts.DTO.CardOutputDTO>> GetAllPublicDtoAsync(CancellationToken ct = default);
}