using Microsoft.EntityFrameworkCore;
using TCGPocketDex.Api.Old.Data;
using TCGPocketDex.Api.Old.Entity;
using TCGPocketDex.Contracts.References;

namespace TCGPocketDex.Api.Old.Repositories;

public class PokemonAbilityRepository(ApplicationDbContext db) : IPokemonAbilityRepository
{
    public async Task<IReadOnlyList<PokemonAbilityOutputDTO>> GetAllAsync(string culture, CancellationToken ct)
    {
        var list = await db.PokemonAbilities
            .AsNoTracking()
            .Include(a => a.Translations)
            .ToListAsync(ct);
        var result = new List<PokemonAbilityOutputDTO>(list.Count);
        foreach (var a in list)
        {
            var tr = a.Translations.FirstOrDefault(x => x.Culture == culture) ?? a.Translations.FirstOrDefault();
            result.Add(new PokemonAbilityOutputDTO(a.Id, tr?.Name ?? string.Empty));
        }
        return result;
    }

    public async Task<PokemonAbilityOutputDTO> CreateAsync(PokemonAbilityInputDTO input, CancellationToken ct)
    {
        var entity = new PokemonAbility();
        entity.Translations.Add(new PokemonAbilityTranslation
        {
            PokemonAbility = entity,
            Culture = input.Culture,
            Name = input.Name
        });
        db.PokemonAbilities.Add(entity);
        await db.SaveChangesAsync(ct);
        return new PokemonAbilityOutputDTO(entity.Id, input.Name);
    }
}
