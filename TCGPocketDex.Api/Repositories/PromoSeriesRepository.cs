using Microsoft.EntityFrameworkCore;
using TCGPocketDex.Api.Data;
using TCGPocketDex.Api.Entity;
using TCGPocketDex.Contracts.References;

namespace TCGPocketDex.Api.Repositories;

public class PromoSeriesRepository(ApplicationDbContext db) : IPromoSeriesRepository
{
    public async Task<IReadOnlyList<PromoSeriesOutputDTO>> GetAllAsync(string culture, CancellationToken ct)
    {
        var list = await db.PromoSeries
            .AsNoTracking()
            .Include(p => p.Translations)
            .ToListAsync(ct);
        var result = new List<PromoSeriesOutputDTO>(list.Count);
        foreach (var p in list)
        {
            var tr = p.Translations.FirstOrDefault(x => x.Culture == culture) ?? p.Translations.FirstOrDefault();
            result.Add(new PromoSeriesOutputDTO(p.Id, p.Code, tr?.Name ?? string.Empty));
        }
        return result;
    }

    public async Task<PromoSeriesOutputDTO> CreateAsync(PromoSeriesInputDTO input, CancellationToken ct)
    {
        var entity = new PromoSeries
        {
            Code = input.Code
        };
        entity.Translations.Add(new PromoSeriesTranslation
        {
            PromoSeries = entity,
            Culture = input.Culture,
            Name = input.Name
        });
        db.PromoSeries.Add(entity);
        await db.SaveChangesAsync(ct);
        return new PromoSeriesOutputDTO(entity.Id, entity.Code, input.Name);
    }
}
