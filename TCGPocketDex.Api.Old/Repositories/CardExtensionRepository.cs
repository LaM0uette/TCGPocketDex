using Microsoft.EntityFrameworkCore;
using TCGPocketDex.Api.Old.Data;
using TCGPocketDex.Api.Old.Entity;
using TCGPocketDex.Contracts.References;

namespace TCGPocketDex.Api.Old.Repositories;

public class CardExtensionRepository(ApplicationDbContext db) : ICardExtensionRepository
{
    public async Task<IReadOnlyList<CardExtensionOutputDTO>> GetAllAsync(string culture, CancellationToken ct)
    {
        var list = await db.CardExtensions
            .AsNoTracking()
            .Include(e => e.Translations)
            .ToListAsync(ct);
        var result = new List<CardExtensionOutputDTO>(list.Count);
        foreach (var e in list)
        {
            var tr = e.Translations.FirstOrDefault(x => x.Culture == culture) ?? e.Translations.FirstOrDefault();
            result.Add(new CardExtensionOutputDTO(e.Id, e.Series, e.Code, tr?.Name ?? string.Empty, tr?.ImageUrl));
        }
        return result;
    }

    public async Task<CardExtensionOutputDTO> CreateAsync(CardExtensionInputDTO input, CancellationToken ct)
    {
        var entity = new CardExtension
        {
            Series = input.Series,
            Code = input.Code
        };
        entity.Translations.Add(new CardExtensionTranslation
        {
            CardExtension = entity,
            Culture = input.Culture,
            Name = input.Name,
            ImageUrl = input.ImageUrl
        });
        db.CardExtensions.Add(entity);
        await db.SaveChangesAsync(ct);
        return new CardExtensionOutputDTO(entity.Id, entity.Series, entity.Code, input.Name, input.ImageUrl);
    }
}
