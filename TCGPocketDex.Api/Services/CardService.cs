using TCGPocketDex.Api.Entities;
using TCGPocketDex.Api.Mappings;
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

        CardCollection? set = await repo.FindSetAsync(dto.CardCollectionId, ct);
        if (set is null) 
            throw new ArgumentException($"CardSetId {dto.CardCollectionId} not found");

        PokemonType? type = await repo.FindPokemonTypeAsync(dto.PokemonTypeId, ct);
        if (type is null) throw new ArgumentException($"TypeId {dto.PokemonTypeId} not found");

        PokemonType? weakness = null;
        if (dto.WeaknessPokemonTypeId is not null)
        {
            weakness = await repo.FindPokemonTypeAsync(dto.WeaknessPokemonTypeId.Value, ct);
            if (weakness is null) throw new ArgumentException($"WeaknessTypeId {dto.WeaknessPokemonTypeId} not found");
        }

        PokemonAbility? ability = null;
        if (dto.PokemonAbilityId is not null)
        {
            ability = await repo.FindPokemonAbilityAsync(dto.PokemonAbilityId.Value, ct);
            if (ability is null) throw new ArgumentException($"PokemonAbilityId {dto.PokemonAbilityId} not found");
        }

        var pokemonTypeEntity = await repo.FindCardTypeByNameAsync("Pokemon", ct) ?? throw new ArgumentException("CardType 'Pokemon' not found");

        var card = new Card
        {
            CardTypeId = pokemonTypeEntity.Id,
            Type = pokemonTypeEntity,
            Name = dto.Name,
            Description = dto.Description ?? string.Empty,
            CardRarityId = dto.CardRarityId,
            Rarity = rarity,
            CardCollectionId = dto.CardCollectionId,
            Collection = set,
            CollectionNumber = dto.CollectionNumber,
        };

        // Card specials
        if (dto.CardSpecialIds?.Count > 0)
        {
            card.Specials = await repo.FindCardSpecialsByIdsAsync(dto.CardSpecialIds, ct);
        }

        await repo.AddCardAsync(card, ct);

        var stage = await repo.FindPokemonStageAsync(dto.PokemonStageId, ct);
        if (stage is null) throw new ArgumentException($"PokemonStageId {dto.PokemonStageId} not found");

        var pokemon = new CardPokemon
        {
            Card = card,
            PokemonStageId = dto.PokemonStageId,
            Stage = stage,
            Hp = dto.Hp,
            PokemonTypeId = dto.PokemonTypeId,
            Type = type,
            WeaknessPokemonTypeId = dto.WeaknessPokemonTypeId,
            Weakness = weakness,
            RetreatCost = dto.RetreatCost,
            PokemonAbilityId = dto.PokemonAbilityId,
            Ability = ability,
        };

        // Pokemon specials
        if (dto.PokemonSpecialIds?.Count > 0)
        {
            pokemon.Specials = await repo.FindPokemonSpecialsByIdsAsync(dto.PokemonSpecialIds, ct);
        }

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

        return card.ToPokemonOutputDTO(pokemon);
    }

    public async Task<CardFossilOutputDTO> CreateFossilAsync(CardFossilInputDTO dto, CancellationToken ct = default)
    {
        CardRarity? rarity = await repo.FindRarityAsync(dto.CardRarityId, ct);
        if (rarity is null) throw new ArgumentException($"CardRarityId {dto.CardRarityId} not found");

        CardCollection? set = await repo.FindSetAsync(dto.CardCollectionId, ct);
        if (set is null) 
            throw new ArgumentException($"CardSetId {dto.CardCollectionId} not found");

        var fossilType = await repo.FindCardTypeByNameAsync("Fossil", ct) ?? throw new ArgumentException("CardType 'Fossil' not found");

        var card = new Card
        {
            CardTypeId = fossilType.Id,
            Type = fossilType,
            Name = dto.Name,
            Description = dto.Description ?? string.Empty,
            CardRarityId = dto.CardRarityId,
            Rarity = rarity,
            CardCollectionId = dto.CardCollectionId,
            Collection = set,
            CollectionNumber = dto.CollectionNumber,
        };
        if (dto.CardSpecialIds?.Count > 0)
        {
            card.Specials = await repo.FindCardSpecialsByIdsAsync(dto.CardSpecialIds, ct);
        }
        await repo.AddCardAsync(card, ct);

        var fossil = new CardFossil
        {
            Card = card,
            Hp = dto.Hp
        };
        await repo.AddFossilAsync(fossil, ct);
        await repo.SaveChangesAsync(ct);

        return card.ToFossilOutputDTO(fossil);
    }

    public async Task<CardToolOutputDTO> CreateToolAsync(CardToolInputDTO dto, CancellationToken ct = default)
    {
        CardRarity? rarity = await repo.FindRarityAsync(dto.CardRarityId, ct);
        if (rarity is null) throw new ArgumentException($"CardRarityId {dto.CardRarityId} not found");

        CardCollection? set = await repo.FindSetAsync(dto.CardCollectionId, ct);
        if (set is null) 
            throw new ArgumentException($"CardSetId {dto.CardCollectionId} not found");

        var toolType = await repo.FindCardTypeByNameAsync("Tool", ct) ?? throw new ArgumentException("CardType 'Tool' not found");

        var card = new Card
        {
            CardTypeId = toolType.Id,
            Type = toolType,
            Name = dto.Name,
            Description = dto.Description ?? string.Empty,
            CardRarityId = dto.CardRarityId,
            Rarity = rarity,
            CardCollectionId = dto.CardCollectionId,
            Collection = set,
            CollectionNumber = dto.CollectionNumber,
        };
        if (dto.CardSpecialIds?.Count > 0)
        {
            card.Specials = await repo.FindCardSpecialsByIdsAsync(dto.CardSpecialIds, ct);
        }
        await repo.AddCardAsync(card, ct);

        var tool = new CardTool { Card = card };
        await repo.AddToolAsync(tool, ct);
        await repo.SaveChangesAsync(ct);

        return card.ToToolOutputDTO(tool);
    }

    public async Task<CardItemOutputDTO> CreateItemAsync(CardItemInputDTO dto, CancellationToken ct = default)
    {
        CardRarity? rarity = await repo.FindRarityAsync(dto.CardRarityId, ct);
        if (rarity is null) throw new ArgumentException($"CardRarityId {dto.CardRarityId} not found");

        CardCollection? set = await repo.FindSetAsync(dto.CardCollectionId, ct);
        if (set is null) 
            throw new ArgumentException($"CardSetId {dto.CardCollectionId} not found");

        var itemType = await repo.FindCardTypeByNameAsync("Item", ct) ?? throw new ArgumentException("CardType 'Item' not found");

        var card = new Card
        {
            CardTypeId = itemType.Id,
            Type = itemType,
            Name = dto.Name,
            Description = dto.Description ?? string.Empty,
            CardRarityId = dto.CardRarityId,
            Rarity = rarity,
            CardCollectionId = dto.CardCollectionId,
            Collection = set,
            CollectionNumber = dto.CollectionNumber,
        };
        await repo.AddCardAsync(card, ct);

        var item = new CardItem { Card = card };
        await repo.AddItemAsync(item, ct);
        await repo.SaveChangesAsync(ct);

        return card.ToItemOutputDTO(item);
    }

    public async Task<CardSupporterOutputDTO> CreateSupporterAsync(CardSupporterInputDTO dto, CancellationToken ct = default)
    {
        CardRarity? rarity = await repo.FindRarityAsync(dto.CardRarityId, ct);
        if (rarity is null) throw new ArgumentException($"CardRarityId {dto.CardRarityId} not found");

        CardCollection? set = await repo.FindSetAsync(dto.CardCollectionId, ct);
        if (set is null) 
                throw new ArgumentException($"CardSetId {dto.CardCollectionId} not found");

        var supporterType = await repo.FindCardTypeByNameAsync("Supporter", ct) ?? throw new ArgumentException("CardType 'Supporter' not found");

        var card = new Card
        {
            CardTypeId = supporterType.Id,
            Type = supporterType,
            Name = dto.Name,
            Description = dto.Description ?? string.Empty,
            CardRarityId = dto.CardRarityId,
            Rarity = rarity,
            CardCollectionId = dto.CardCollectionId,
            Collection = set,
            CollectionNumber = dto.CollectionNumber,
        };
        if (dto.CardSpecialIds?.Count > 0)
        {
            card.Specials = await repo.FindCardSpecialsByIdsAsync(dto.CardSpecialIds, ct);
        }
        await repo.AddCardAsync(card, ct);

        var supporter = new CardSupporter { Card = card };
        await repo.AddSupporterAsync(supporter, ct);
        await repo.SaveChangesAsync(ct);

        return card.ToSupporterOutputDTO(supporter);
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
            Culture = translation.Culture,
            Name = translation.Name,
            Description = translation.Description,
        };
    }

}
