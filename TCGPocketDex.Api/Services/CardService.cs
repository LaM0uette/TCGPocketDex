using Microsoft.EntityFrameworkCore;
using TCGPocketDex.Api.Entity;
using TCGPocketDex.Api.Repositories;
using TCGPocketDex.Contracts.DTO;

namespace TCGPocketDex.Api.Services;

public class CardService(ICardRepository repo) : ICardService
{
    public async Task<CardPokemonOutputDTO> CreatePokemonAsync(CardPokemonInputDTO dto, CancellationToken ct = default)
    {
        // Validate required FK
        CardRarity? rarity = await repo.FindRarityAsync(dto.CardRarityId, ct);
        if (rarity is null) throw new ArgumentException($"CardRarityId {dto.CardRarityId} not found");

        CardSet? set = null;
        if (dto.CardSetId is not null)
        {
            set = await repo.FindSetAsync(dto.CardSetId.Value, ct);
            if (set is null) throw new ArgumentException($"CardSetId {dto.CardSetId} not found");
        }

        PokemonType? type = await repo.FindPokemonTypeAsync(dto.TypeId, ct);
        if (type is null) throw new ArgumentException($"TypeId {dto.TypeId} not found");

        PokemonType? weakness = null;
        if (dto.WeaknessTypeId is not null)
        {
            weakness = await repo.FindPokemonTypeAsync(dto.WeaknessTypeId.Value, ct);
            if (weakness is null) throw new ArgumentException($"WeaknessTypeId {dto.WeaknessTypeId} not found");
        }

        PokemonAbility? ability = null;
        if (dto.PokemonAbilityId is not null)
        {
            ability = await repo.FindPokemonAbilityAsync(dto.PokemonAbilityId.Value, ct);
            if (ability is null) throw new ArgumentException($"PokemonAbilityId {dto.PokemonAbilityId} not found");
        }

        var card = new Card
        {
            Kind = CardKind.Pokemon,
            Name = dto.Name,
            Description = dto.Description,
            Specials = dto.IsPromo ? CardSpecial.Promo : CardSpecial.None,
            CardRarityId = dto.CardRarityId,
            Rarity = rarity,
            CardSetId = dto.CardSetId,
            CardSet = set,
            SerieNumber = dto.SerieNumber,
        };

        await repo.AddCardAsync(card, ct);

        var pokemon = new CardPokemon
        {
            Card = card,
            Specials = (PokemonSpecial)dto.PokemonSpecials,
            Stage = (PokemonStage)dto.Stage,
            Hp = dto.Hp,
            TypeId = dto.TypeId,
            Type = type,
            WeaknessTypeId = dto.WeaknessTypeId,
            Weakness = weakness,
            RetreatCost = dto.RetreatCost,
            PokemonAbilityId = dto.PokemonAbilityId,
            Ability = ability,
        };

        // Map attacks
        if (dto.Attacks?.Count > 0)
        {
            foreach (var atkDto in dto.Attacks)
            {
                var attack = new PokemonAttack
                {
                    Name = atkDto.Name,
                    Description = atkDto.Description,
                    Damage = atkDto.Damage,
                };

                // Load costs types
                if (atkDto.Costs?.Count > 0)
                {
                    var costTypes = new List<PokemonType>();
                    foreach (int tId in atkDto.Costs)
                    {
                        var t = await repo.FindPokemonTypeAsync(tId, ct) ?? throw new ArgumentException($"Attack cost TypeId {tId} not found");
                        costTypes.Add(t);
                    }
                    attack.Costs = costTypes;
                }

                pokemon.Attacks.Add(attack);
            }
        }

        await repo.AddPokemonAsync(pokemon, ct);
        await repo.SaveChangesAsync(ct);

        return MapPokemon(card, pokemon);
    }

    public async Task<CardFossilOutputDTO> CreateFossilAsync(CardFossilInputDTO dto, CancellationToken ct = default)
    {
        CardRarity? rarity = await repo.FindRarityAsync(dto.CardRarityId, ct);
        if (rarity is null) throw new ArgumentException($"CardRarityId {dto.CardRarityId} not found");

        CardSet? set = null;
        if (dto.CardSetId is not null)
        {
            set = await repo.FindSetAsync(dto.CardSetId.Value, ct);
            if (set is null) throw new ArgumentException($"CardSetId {dto.CardSetId} not found");
        }

        var card = new Card
        {
            Kind = CardKind.Fossil,
            Name = dto.Name,
            Description = dto.Description,
            Specials = dto.IsPromo ? CardSpecial.Promo : CardSpecial.None,
            CardRarityId = dto.CardRarityId,
            Rarity = rarity,
            CardSetId = dto.CardSetId,
            CardSet = set,
            SerieNumber = dto.SerieNumber,
        };
        await repo.AddCardAsync(card, ct);

        var fossil = new CardFossil
        {
            Card = card,
            Hp = dto.Hp
        };
        await repo.AddFossilAsync(fossil, ct);
        await repo.SaveChangesAsync(ct);

        return new CardFossilOutputDTO
        {
            Id = card.Id,
            Name = card.Name,
            Description = card.Description,
            IsPromo = (card.Specials & CardSpecial.Promo) != 0,
            CardRarityId = card.CardRarityId,
            CardSetId = card.CardSetId,
            SerieNumber = card.SerieNumber,
            Hp = fossil.Hp
        };
    }

    public async Task<CardToolOutputDTO> CreateToolAsync(CardToolInputDTO dto, CancellationToken ct = default)
    {
        CardRarity? rarity = await repo.FindRarityAsync(dto.CardRarityId, ct);
        if (rarity is null) throw new ArgumentException($"CardRarityId {dto.CardRarityId} not found");

        CardSet? set = null;
        if (dto.CardSetId is not null)
        {
            set = await repo.FindSetAsync(dto.CardSetId.Value, ct);
            if (set is null) throw new ArgumentException($"CardSetId {dto.CardSetId} not found");
        }

        var card = new Card
        {
            Kind = CardKind.Tool,
            Name = dto.Name,
            Description = dto.Description,
            Specials = dto.IsPromo ? CardSpecial.Promo : CardSpecial.None,
            CardRarityId = dto.CardRarityId,
            Rarity = rarity,
            CardSetId = dto.CardSetId,
            CardSet = set,
            SerieNumber = dto.SerieNumber,
        };
        await repo.AddCardAsync(card, ct);

        var tool = new CardTool { Card = card };
        await repo.AddToolAsync(tool, ct);
        await repo.SaveChangesAsync(ct);

        return new CardToolOutputDTO
        {
            Id = card.Id,
            Name = card.Name,
            Description = card.Description,
            IsPromo = (card.Specials & CardSpecial.Promo) != 0,
            CardRarityId = card.CardRarityId,
            CardSetId = card.CardSetId,
            SerieNumber = card.SerieNumber,
        };
    }

    public async Task<CardItemOutputDTO> CreateItemAsync(CardItemInputDTO dto, CancellationToken ct = default)
    {
        CardRarity? rarity = await repo.FindRarityAsync(dto.CardRarityId, ct);
        if (rarity is null) throw new ArgumentException($"CardRarityId {dto.CardRarityId} not found");

        CardSet? set = null;
        if (dto.CardSetId is not null)
        {
            set = await repo.FindSetAsync(dto.CardSetId.Value, ct);
            if (set is null) throw new ArgumentException($"CardSetId {dto.CardSetId} not found");
        }

        var card = new Card
        {
            Kind = CardKind.Item,
            Name = dto.Name,
            Description = dto.Description,
            Specials = dto.IsPromo ? CardSpecial.Promo : CardSpecial.None,
            CardRarityId = dto.CardRarityId,
            Rarity = rarity,
            CardSetId = dto.CardSetId,
            CardSet = set,
            SerieNumber = dto.SerieNumber,
        };
        await repo.AddCardAsync(card, ct);

        var item = new CardItem { Card = card };
        await repo.AddItemAsync(item, ct);
        await repo.SaveChangesAsync(ct);

        return new CardItemOutputDTO
        {
            Id = card.Id,
            Name = card.Name,
            Description = card.Description,
            IsPromo = (card.Specials & CardSpecial.Promo) != 0,
            CardRarityId = card.CardRarityId,
            CardSetId = card.CardSetId,
            SerieNumber = card.SerieNumber,
        };
    }

    public async Task<CardSupporterOutputDTO> CreateSupporterAsync(CardSupporterInputDTO dto, CancellationToken ct = default)
    {
        CardRarity? rarity = await repo.FindRarityAsync(dto.CardRarityId, ct);
        if (rarity is null) throw new ArgumentException($"CardRarityId {dto.CardRarityId} not found");

        CardSet? set = null;
        if (dto.CardSetId is not null)
        {
            set = await repo.FindSetAsync(dto.CardSetId.Value, ct);
            if (set is null) throw new ArgumentException($"CardSetId {dto.CardSetId} not found");
        }

        var card = new Card
        {
            Kind = CardKind.Supporter,
            Name = dto.Name,
            Description = dto.Description,
            Specials = dto.IsPromo ? CardSpecial.Promo : CardSpecial.None,
            CardRarityId = dto.CardRarityId,
            Rarity = rarity,
            CardSetId = dto.CardSetId,
            CardSet = set,
            SerieNumber = dto.SerieNumber,
        };
        await repo.AddCardAsync(card, ct);

        var supporter = new CardSupporter { Card = card };
        await repo.AddSupporterAsync(supporter, ct);
        await repo.SaveChangesAsync(ct);

        return new CardSupporterOutputDTO
        {
            Id = card.Id,
            Name = card.Name,
            Description = card.Description,
            IsPromo = (card.Specials & CardSpecial.Promo) != 0,
            CardRarityId = card.CardRarityId,
            CardSetId = card.CardSetId,
            SerieNumber = card.SerieNumber,
        };
    }

    public async Task<CardTranslationOutputDTO> AddCardTranslationAsync(int cardId, CardTranslationInputDTO dto, CancellationToken ct = default)
    {
        var card = await repo.FindCardAsync(cardId, ct) ?? throw new ArgumentException($"CardId {cardId} not found");

        var translation = new CardTranslation
        {
            Card = card,
            CardId = card.Id,
            Culture = dto.Culture,
            Name = dto.Name,
            Description = dto.Description,
        };

        await repo.AddCardTranslationAsync(translation, ct);
        await repo.SaveChangesAsync(ct);

        return new CardTranslationOutputDTO
        {
            Id = translation.Id,
            CardId = card.Id,
            Culture = translation.Culture,
            Name = translation.Name,
            Description = translation.Description,
        };
    }

    private static CardPokemonOutputDTO MapPokemon(Card card, CardPokemon pokemon)
    {
        return new CardPokemonOutputDTO
        {
            Id = card.Id,
            Name = card.Name,
            Description = card.Description,
            IsPromo = (card.Specials & CardSpecial.Promo) != 0,
            CardRarityId = card.CardRarityId,
            CardSetId = card.CardSetId,
            SerieNumber = card.SerieNumber,
            Specials = (int)pokemon.Specials,
            Stage = (int)pokemon.Stage,
            Hp = pokemon.Hp,
            TypeId = pokemon.TypeId,
            WeaknessTypeId = pokemon.WeaknessTypeId,
            RetreatCost = pokemon.RetreatCost,
            PokemonAbilityId = pokemon.PokemonAbilityId,
            Attacks = pokemon.Attacks.Select(a => new PokemonAttackOutputDTO
            {
                Id = a.Id,
                Name = a.Name,
                Description = a.Description,
                Damage = a.Damage,
                Costs = a.Costs.Select(c => c.Id).ToList(),
            }).ToList()
        };
    }
}
