using Microsoft.EntityFrameworkCore;
using TCGPocketDex.Api.Old.Data;
using TCGPocketDex.Api.Old.Entity;
using TCGPocketDex.Contracts.Cards;

namespace TCGPocketDex.Api.Old.Repositories;

public class CardRepository(ApplicationDbContext db) : ICardRepository
{
    public async Task<IReadOnlyList<CardOutputDTO>> GetAllAsync(string culture, CancellationToken ct)
    {
        var list = await db.Cards
            .AsNoTracking()
            .Include(c => c.Rarity)
            .Include(c => c.Translations)
            .Include(c => c.Booster)!.ThenInclude(b => b!.Translations)
            .Include(c => c.Extension)!.ThenInclude(e => e!.Translations)
            .Include(c => c.PromoSeries)!.ThenInclude(p => p!.Translations)
            .ToListAsync(ct);

        var result = new List<CardOutputDTO>(list.Count);
        foreach (var c in list)
        {
            if (c is PokemonCard pc)
            {
                await db.Entry(pc).Reference(x => x.Weakness).LoadAsync(ct);
                await db.Entry(pc).Collection(x => x.Attacks).LoadAsync(ct);
            }
            result.Add(ToOutput(c, culture));
        }
        return result;
    }

    public async Task<CardOutputDTO?> GetByIdAsync(int id, string culture, CancellationToken ct)
    {
        var c = await db.Cards
            .AsNoTracking()
            .Include(x => x.Rarity)
            .Include(x => x.Translations)
            .Include(x => x.Booster)!.ThenInclude(b => b!.Translations)
            .Include(x => x.Extension)!.ThenInclude(e => e!.Translations)
            .Include(x => x.PromoSeries)!.ThenInclude(p => p!.Translations)
            .FirstOrDefaultAsync(x => x.Id == id, ct);
        if (c is PokemonCard pc)
        {
            await db.Entry(pc).Reference(x => x.Weakness).LoadAsync(ct);
            await db.Entry(pc).Collection(x => x.Attacks).LoadAsync(ct);
        }
        return c == null ? null : ToOutput(c, culture);
    }

    public async Task<CardOutputDTO> CreateAsync(CardInputDTO input, CancellationToken ct)
    {
        ValidateOrigin(input);

        Card card = input.Kind switch
        {
            CardKind.Supporter => new SupporterCard { Rarity = null! },
            CardKind.Item => new ItemCard { Rarity = null! },
            CardKind.PokemonTool => new PokemonToolCard { Rarity = null! },
            CardKind.Fossil => new FossilCard { Rarity = null!, Hp = input.FossilHp ?? 0 },
            CardKind.Pokemon => new PokemonCard
            {
                Rarity = null!,
                IsEx = input.PokemonIsEx ?? false,
                IsMega = input.PokemonIsMega ?? false,
                StageId = input.PokemonStageId ?? 0,
                Stage = null!,
                Hp = input.PokemonHp ?? 0,
                TypeId = input.PokemonTypeId ?? 0,
                Type = null!,
                RetreatCost = input.PokemonRetreatCost ?? 0,
                PokemonAbilityId = input.PokemonAbilityId
            },
            _ => throw new InvalidOperationException("Unknown card kind")
        };

        card.CardRarityId = input.CardRarityId;
        card.BoosterId = input.BoosterId;
        card.CardExtensionId = input.CardExtensionId;
        card.ExtensionCardNumber = input.ExtensionCardNumber;
        card.PromoSeriesId = input.PromoSeriesId;
        card.Rarity = null!;
        card.Translations.Add(new CardTranslation
        {
            Card = card,
            Culture = input.Culture,
            Name = input.Name,
            Description = input.Description,
            ImageUrl = input.ImageUrl
        });

        if (card is PokemonCard pc)
        {
            if (input.PokemonWeaknessTypeId.HasValue)
                pc.Weakness = new PokemonWeakness { Pokemon = pc, TypeId = input.PokemonWeaknessTypeId.Value, Type = null! };
            if (input.PokemonAttackIds != null && input.PokemonAttackIds.Count > 0)
            {
                var attacks = await db.PokemonAttacks.Where(a => input.PokemonAttackIds.Contains(a.Id)).ToListAsync(ct);
                foreach (var a in attacks) pc.Attacks.Add(a);
            }
        }

        db.Cards.Add(card);
        await db.SaveChangesAsync(ct);

        if (card is PokemonCard pc2)
        {
            await db.Entry(pc2).Reference(x => x.Weakness).LoadAsync(ct);
            await db.Entry(pc2).Collection(x => x.Attacks).LoadAsync(ct);
        }

        var loaded = await db.Cards
            .Include(x => x.Rarity)
            .Include(x => x.Translations)
            .Include(x => x.Booster)!.ThenInclude(b => b!.Translations)
            .Include(x => x.Extension)!.ThenInclude(e => e!.Translations)
            .Include(x => x.PromoSeries)!.ThenInclude(p => p!.Translations)
            .FirstAsync(x => x.Id == card.Id, ct);

        return ToOutput(loaded, input.Culture);
    }

    public async Task<CardOutputDTO?> UpdateAsync(int id, CardInputDTO input, CancellationToken ct)
    {
        ValidateOrigin(input);

        var card = await db.Cards
            .Include(x => x.Translations)
            .FirstOrDefaultAsync(x => x.Id == id, ct);
        if (card == null) return null;

        var kind = card switch
        {
            SupporterCard => CardKind.Supporter,
            ItemCard => CardKind.Item,
            PokemonToolCard => CardKind.PokemonTool,
            FossilCard => CardKind.Fossil,
            PokemonCard => CardKind.Pokemon,
            _ => throw new InvalidOperationException("Unknown card kind")
        };
        if (kind != input.Kind)
            throw new InvalidOperationException("Changing card kind is not supported");

        card.CardRarityId = input.CardRarityId;
        card.BoosterId = input.BoosterId;
        card.CardExtensionId = input.CardExtensionId;
        card.ExtensionCardNumber = input.ExtensionCardNumber;
        card.PromoSeriesId = input.PromoSeriesId;

        if (card is FossilCard fc)
        {
            fc.Hp = input.FossilHp ?? fc.Hp;
        }
        else if (card is PokemonCard pc)
        {
            pc.IsEx = input.PokemonIsEx ?? pc.IsEx;
            pc.IsMega = input.PokemonIsMega ?? pc.IsMega;
            if (input.PokemonStageId.HasValue) pc.StageId = input.PokemonStageId.Value;
            if (input.PokemonHp.HasValue) pc.Hp = input.PokemonHp.Value;
            if (input.PokemonTypeId.HasValue) pc.TypeId = input.PokemonTypeId.Value;
            if (input.PokemonRetreatCost.HasValue) pc.RetreatCost = input.PokemonRetreatCost.Value;
            pc.PokemonAbilityId = input.PokemonAbilityId;

            if (input.PokemonWeaknessTypeId.HasValue)
            {
                if (pc.Weakness == null)
                    pc.Weakness = new PokemonWeakness { Pokemon = pc, TypeId = input.PokemonWeaknessTypeId.Value, Type = null! };
                else
                    pc.Weakness.TypeId = input.PokemonWeaknessTypeId.Value;
            }
            else
            {
                pc.Weakness = null;
            }

            if (input.PokemonAttackIds != null)
            {
                await db.Entry(pc).Collection(x => x.Attacks).LoadAsync(ct);
                pc.Attacks.Clear();
                if (input.PokemonAttackIds.Count > 0)
                {
                    var attacks = await db.PokemonAttacks.Where(a => input.PokemonAttackIds.Contains(a.Id)).ToListAsync(ct);
                    foreach (var a in attacks) pc.Attacks.Add(a);
                }
            }
        }

        var tr = card.Translations.FirstOrDefault(t => t.Culture == input.Culture);
        if (tr == null)
        {
            tr = new CardTranslation
            {
                Culture = input.Culture,
                Name = input.Name,
                Description = input.Description,
                ImageUrl = input.ImageUrl,
                Card = card
            };
            db.CardTranslations.Add(tr);
        }
        else
        {
            tr.Name = input.Name;
            tr.Description = input.Description;
            tr.ImageUrl = input.ImageUrl;
        }

        await db.SaveChangesAsync(ct);

        var loaded = await db.Cards
            .AsNoTracking()
            .Include(x => x.Rarity)
            .Include(x => x.Translations)
            .Include(x => x.Booster)!.ThenInclude(b => b!.Translations)
            .Include(x => x.Extension)!.ThenInclude(e => e!.Translations)
            .Include(x => x.PromoSeries)!.ThenInclude(p => p!.Translations)
            .FirstAsync(x => x.Id == id, ct);
        if (loaded is PokemonCard pcLoaded)
        {
            await db.Entry(pcLoaded).Reference(x => x.Weakness).LoadAsync(ct);
            await db.Entry(pcLoaded).Collection(x => x.Attacks).LoadAsync(ct);
        }
        return ToOutput(loaded, input.Culture);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct)
    {
        var card = await db.Cards.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (card == null) return false;
        db.Cards.Remove(card);
        await db.SaveChangesAsync(ct);
        return true;
    }

    public async Task<CardOutputDTO?> AddTranslationAsync(int id, CardTranslationInputDTO input, CancellationToken ct)
    {
        var card = await db.Cards
            .Include(x => x.Translations)
            .FirstOrDefaultAsync(x => x.Id == id, ct);
        if (card == null) return null;

        // Find existing translation for same culture
        var existing = card.Translations.FirstOrDefault(t => t.Culture == input.Culture);
        // Decide ImageUrl: use provided or copy from EN or first available
        string imageUrl = !string.IsNullOrWhiteSpace(input.ImageUrl)
            ? input.ImageUrl!
            : (card.Translations.FirstOrDefault(t => t.Culture == "en")?.ImageUrl
               ?? card.Translations.FirstOrDefault()?.ImageUrl
               ?? string.Empty);

        if (existing == null)
        {
            var tr = new CardTranslation
            {
                Card = card,
                Culture = input.Culture,
                Name = input.Name,
                Description = input.Description,
                ImageUrl = imageUrl
            };
            db.CardTranslations.Add(tr);
        }
        else
        {
            existing.Name = input.Name;
            existing.Description = input.Description;
            existing.ImageUrl = imageUrl;
        }

        await db.SaveChangesAsync(ct);

        // Return card in requested culture
        var loaded = await db.Cards
            .AsNoTracking()
            .Include(x => x.Rarity)
            .Include(x => x.Translations)
            .Include(x => x.Booster)!.ThenInclude(b => b!.Translations)
            .Include(x => x.Extension)!.ThenInclude(e => e!.Translations)
            .Include(x => x.PromoSeries)!.ThenInclude(p => p!.Translations)
            .FirstAsync(x => x.Id == id, ct);
        if (loaded is PokemonCard pcLoaded)
        {
            await db.Entry(pcLoaded).Reference(x => x.Weakness).LoadAsync(ct);
            await db.Entry(pcLoaded).Collection(x => x.Attacks).LoadAsync(ct);
        }
        return ToOutput(loaded, input.Culture);
    }

    static void ValidateOrigin(CardInputDTO input)
    {
        var hasBooster = input.BoosterId.HasValue && input.CardExtensionId.HasValue;
        var hasPromo = input.PromoSeriesId.HasValue;
        if (!(hasBooster ^ hasPromo))
            throw new InvalidOperationException("Invalid origin");
    }

    static CardOutputDTO ToOutput(Card c, string culture)
    {
        var tr = c.Translations.FirstOrDefault(t => t.Culture == culture) ?? c.Translations.FirstOrDefault();
        var boosterName = c.Booster?.Translations.FirstOrDefault(t => t.Culture == culture)?.Name ?? c.Booster?.Translations.FirstOrDefault()?.Name;
        var extTr = c.Extension?.Translations.FirstOrDefault(t => t.Culture == culture) ?? c.Extension?.Translations.FirstOrDefault();
        var promoName = c.PromoSeries?.Translations.FirstOrDefault(t => t.Culture == culture)?.Name ?? c.PromoSeries?.Translations.FirstOrDefault()?.Name;

        CardKind kind = c switch
        {
            SupporterCard => CardKind.Supporter,
            ItemCard => CardKind.Item,
            PokemonToolCard => CardKind.PokemonTool,
            FossilCard => CardKind.Fossil,
            PokemonCard => CardKind.Pokemon,
            _ => CardKind.Item
        };

        int? fossilHp = null;
        bool? pIsEx = null;
        bool? pIsMega = null;
        int? pStageId = null;
        int? pHp = null;
        int? pTypeId = null;
        int? pWeaknessTypeId = null;
        int? pRetreat = null;
        int? pAbilityId = null;
        IReadOnlyList<int>? pAttackIds = null;
        if (c is FossilCard fc)
        {
            fossilHp = fc.Hp;
        }
        else if (c is PokemonCard pc)
        {
            pIsEx = pc.IsEx;
            pIsMega = pc.IsMega;
            pStageId = pc.StageId;
            pHp = pc.Hp;
            pTypeId = pc.TypeId;
            pRetreat = pc.RetreatCost;
            pAbilityId = pc.PokemonAbilityId;
            pWeaknessTypeId = pc.Weakness?.TypeId;
            pAttackIds = pc.Attacks.Select(a => a.Id).ToList();
        }

        return new CardOutputDTO(
            c.Id,
            culture,
            tr?.Name ?? string.Empty,
            tr?.Description,
            //tr?.ImageUrl ?? string.Empty,
            $"https://localhost:7093/img/cards/{c.Extension?.Code}-{c.ExtensionCardNumber}.webp", // mettre le code promo si non null
            c.CardRarityId,
            c.Rarity.Name,
            c.BoosterId,
            boosterName,
            c.CardExtensionId,
            c.Extension?.Series,
            c.Extension?.Code,
            extTr?.Name,
            c.ExtensionCardNumber,
            c.PromoSeriesId,
            c.PromoSeries?.Code,
            promoName,
            kind,
            fossilHp,
            pIsEx,
            pIsMega,
            pStageId,
            pHp,
            pTypeId,
            pWeaknessTypeId,
            pRetreat,
            pAbilityId,
            pAttackIds
        );
    }
}
