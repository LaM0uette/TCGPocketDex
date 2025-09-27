using Microsoft.EntityFrameworkCore;
using TCGPocketDex.Api.Data;
using TCGPocketDex.Contracts.References;

namespace TCGPocketDex.Api.Repositories;

public class BoosterRepository(ApplicationDbContext db) : IBoosterRepository
{
    public async Task<IReadOnlyList<BoosterOutputDTO>> GetAllAsync(string culture, int? cardExtensionId, CancellationToken ct)
    {
        var query = db.Boosters.AsNoTracking().Include(b => b.Translations).AsQueryable();
        if (cardExtensionId.HasValue)
            query = query.Where(b => b.CardExtensionId == cardExtensionId.Value);
        var list = await query.ToListAsync(ct);
        var result = new List<BoosterOutputDTO>(list.Count);
        foreach (var b in list)
        {
            var tr = b.Translations.FirstOrDefault(x => x.Culture == culture) ?? b.Translations.FirstOrDefault();
            result.Add(new BoosterOutputDTO(b.Id, b.CardExtensionId, tr?.Name ?? string.Empty, tr?.ImageUrl));
        }
        return result;
    }

    public async Task<BoosterOutputDTO> CreateAsync(BoosterInputDTO input, CancellationToken ct)
    {
        var entity = new TCGPocketDex.Api.Entity.Booster
        {
            CardExtensionId = input.CardExtensionId,
            CardExtension = null!
        };
        entity.Translations.Add(new TCGPocketDex.Api.Entity.BoosterTranslation
        {
            Booster = entity,
            Culture = input.Culture,
            Name = input.Name,
            ImageUrl = input.ImageUrl
        });
        db.Boosters.Add(entity);
        await db.SaveChangesAsync(ct);
        return new BoosterOutputDTO(entity.Id, entity.CardExtensionId, input.Name, input.ImageUrl);
    }
}
