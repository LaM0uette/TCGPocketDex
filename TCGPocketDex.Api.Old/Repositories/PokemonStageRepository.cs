using Microsoft.EntityFrameworkCore;
using TCGPocketDex.Api.Old.Data;
using TCGPocketDex.Api.Old.Entity;
using TCGPocketDex.Contracts.References;

namespace TCGPocketDex.Api.Old.Repositories;

public class PokemonStageRepository(ApplicationDbContext db) : IPokemonStageRepository
{
    public async Task<IReadOnlyList<PokemonStageOutputDTO>> GetAllAsync(string culture, CancellationToken ct)
    {
        var list = await db.PokemonStages
            .AsNoTracking()
            .Include(s => s.Translations)
            .ToListAsync(ct);
        var result = new List<PokemonStageOutputDTO>(list.Count);
        foreach (var s in list)
        {
            var tr = s.Translations.FirstOrDefault(x => x.Culture == culture) ?? s.Translations.FirstOrDefault();
            result.Add(new PokemonStageOutputDTO(s.Id, tr?.Name ?? string.Empty));
        }
        return result;
    }

    public async Task<PokemonStageOutputDTO> CreateAsync(PokemonStageInputDTO input, CancellationToken ct)
    {
        var entity = new PokemonStage();
        entity.Translations.Add(new PokemonStageTranslation
        {
            PokemonStage = entity,
            Culture = input.Culture,
            Name = input.Name
        });
        db.PokemonStages.Add(entity);
        await db.SaveChangesAsync(ct);
        return new PokemonStageOutputDTO(entity.Id, input.Name);
    }
}
