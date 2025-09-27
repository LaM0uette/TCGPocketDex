using Microsoft.EntityFrameworkCore;
using TCGPocketDex.Api.Data;
using TCGPocketDex.Api.Entity;
using TCGPocketDex.Contracts.References;

namespace TCGPocketDex.Api.Repositories;

public class PokemonTypeRepository(ApplicationDbContext db) : IPokemonTypeRepository
{
    public async Task<IReadOnlyList<PokemonTypeOutputDTO>> GetAllAsync(string culture, CancellationToken ct)
    {
        var list = await db.PokemonTypes
            .AsNoTracking()
            .Include(t => t.Translations)
            .ToListAsync(ct);
        var result = new List<PokemonTypeOutputDTO>(list.Count);
        foreach (var t in list)
        {
            var tr = t.Translations.FirstOrDefault(x => x.Culture == culture) ?? t.Translations.FirstOrDefault();
            result.Add(new PokemonTypeOutputDTO(t.Id, tr?.Name ?? string.Empty, tr?.ImageUrl ?? string.Empty));
        }
        return result;
    }

    public async Task<PokemonTypeOutputDTO> CreateAsync(PokemonTypeInputDTO input, CancellationToken ct)
    {
        var entity = new PokemonType();
        entity.Translations.Add(new PokemonTypeTranslation
        {
            PokemonType = entity,
            Culture = input.Culture,
            Name = input.Name,
            ImageUrl = input.ImageUrl
        });
        db.PokemonTypes.Add(entity);
        await db.SaveChangesAsync(ct);
        return new PokemonTypeOutputDTO(entity.Id, input.Name, input.ImageUrl);
    }
}
