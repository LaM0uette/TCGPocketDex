using Microsoft.EntityFrameworkCore;
using TCGPocketDex.Api.Data;
using TCGPocketDex.Api.Entity;
using TCGPocketDex.Contracts.References;

namespace TCGPocketDex.Api.Repositories;

public class CardRarityRepository(ApplicationDbContext db) : ICardRarityRepository
{
    public async Task<IReadOnlyList<CardRarityOutputDTO>> GetAllAsync(CancellationToken ct)
    {
        var list = await db.CardRarities.AsNoTracking().ToListAsync(ct);
        var result = new List<CardRarityOutputDTO>(list.Count);
        foreach (var r in list)
        {
            result.Add(new CardRarityOutputDTO(r.Id, r.Name, r.ImageUrl));
        }
        return result;
    }

    public async Task<CardRarityOutputDTO> CreateAsync(CardRarityInputDTO input, CancellationToken ct)
    {
        var entity = new CardRarity
        {
            Name = input.Name,
            ImageUrl = input.ImageUrl
        };
        db.CardRarities.Add(entity);
        await db.SaveChangesAsync(ct);
        return new CardRarityOutputDTO(entity.Id, entity.Name, entity.ImageUrl);
    }
}
