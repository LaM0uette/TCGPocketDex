using Microsoft.EntityFrameworkCore;
using TCGPocketDex.Api.Data;
using TCGPocketDex.Api.Entity;
using TCGPocketDex.Contracts.References;

namespace TCGPocketDex.Api.Repositories;

public class PokemonAttackRepository(ApplicationDbContext db) : IPokemonAttackRepository
{
    public async Task<IReadOnlyList<PokemonAttackOutputDTO>> GetAllAsync(string culture, CancellationToken ct)
    {
        var list = await db.PokemonAttacks
            .AsNoTracking()
            .Include(a => a.Translations)
            .ToListAsync(ct);
        var result = new List<PokemonAttackOutputDTO>(list.Count);
        foreach (var a in list)
        {
            var tr = a.Translations.FirstOrDefault(x => x.Culture == culture) ?? a.Translations.FirstOrDefault();
            result.Add(new PokemonAttackOutputDTO(a.Id, tr?.Name ?? string.Empty, a.Damage));
        }
        return result;
    }

    public async Task<PokemonAttackOutputDTO> CreateAsync(PokemonAttackInputDTO input, CancellationToken ct)
    {
        var entity = new PokemonAttack
        {
            Damage = input.Damage
        };
        entity.Translations.Add(new PokemonAttackTranslation
        {
            PokemonAttack = entity,
            Culture = input.Culture,
            Name = input.Name,
            Description = input.Description
        });
        if (input.CostTypeIds.Count > 0)
        {
            var types = await db.PokemonTypes.Where(t => input.CostTypeIds.Contains(t.Id)).ToListAsync(ct);
            foreach (var t in types) entity.Costs.Add(t);
        }
        db.PokemonAttacks.Add(entity);
        await db.SaveChangesAsync(ct);
        return new PokemonAttackOutputDTO(entity.Id, input.Name, entity.Damage);
    }
}
