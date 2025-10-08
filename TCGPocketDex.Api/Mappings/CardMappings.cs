using TCGPocketDex.Api.Entities;
using TCGPocketDex.Contracts.DTO;

namespace TCGPocketDex.Api.Mappings;

public static class CardMappings
{
    public static CardOutputDTO ToOutputDTO(this Card card)
    {
        return card.ToOutputDTOWithCulture(null);
    }

    public static CardOutputDTO ToOutputDTOWithCulture(this Card card, string? culture)
    {
        string? normalized = NormalizeCulture(culture);

        string name = card.Name;
        string description = card.Description ?? string.Empty;
        var tr = normalized is null ? null : card.Translations.FirstOrDefault(t => string.Equals(t.Culture, normalized, StringComparison.OrdinalIgnoreCase));
        if (tr is not null)
        {
            name = tr.Name;
            description = tr.Description ?? description;
        }

        string typeName = card.Type.Name;
        var typeTr = normalized is null ? null : card.Type.Translations.FirstOrDefault(t => string.Equals(t.Culture, normalized, StringComparison.OrdinalIgnoreCase));
        if (typeTr is not null)
            typeName = typeTr.Name;

        string rarityName = card.Rarity.Name;
        var rarityTr = normalized is null ? null : card.Rarity.Translations.FirstOrDefault(t => string.Equals(t.Culture, normalized, StringComparison.OrdinalIgnoreCase));
        if (rarityTr is not null)
            rarityName = rarityTr.Name;

        string collectionName = card.Collection.Name;
        var collectionTr = normalized is null ? null : card.Collection.Translations.FirstOrDefault(t => string.Equals(t.Culture, normalized, StringComparison.OrdinalIgnoreCase));
        if (collectionTr is not null)
            collectionName = collectionTr.Name;

        return new CardOutputDTO
        {
            Id = card.Id,
            Type = new CardTypeOutputDTO { Name = typeName },
            Name = name,
            Description = description,
            ImageUrl = $"https://tcgp-dex.com/{normalized}/{card.Collection.Code}-{card.CollectionNumber}.webp", // TODO: Move to config
            Specials = card.Specials.Select(s =>
            {
                string sName = s.Name;
                var sTr = normalized is null ? null : s.Translations.FirstOrDefault(t => string.Equals(t.Culture, normalized, StringComparison.OrdinalIgnoreCase));
                if (sTr is not null) sName = sTr.Name;
                return new CardSpecialOutputDTO { Name = sName };
            }).ToList(),
            Rarity = new CardRarityOutputDTO { Name = rarityName },
            Collection = new CardCollectionOutputDTO { Code = card.Collection.Code, Series = card.Collection.Series, Name = collectionName },
            CollectionNumber = card.CollectionNumber,
            Translations = card.Translations.Select(t => new CardTranslationOutputDTO
            {
                Id = t.Id,
                Culture = t.Culture,
                Name = t.Name,
                Description = t.Description ?? string.Empty
            }).ToList()
        };
    }

    private static string? NormalizeCulture(string? culture)
    {
        if (string.IsNullOrWhiteSpace(culture)) return null;
        // keep two-letter lower-case when possible (e.g., fr, en)
        var trimmed = culture.Trim();
        if (trimmed.Length >= 2)
            return trimmed[..2].ToLowerInvariant();
        return trimmed.ToLowerInvariant();
    }

    public static CardPokemonOutputDTO ToPokemonOutputDTO(this Card card, CardPokemon pokemon)
    {
        return new CardPokemonOutputDTO
        {
            Id = card.Id,
            Type = new CardTypeOutputDTO { Name = card.Type.Name },
            Name = card.Name,
            Description = card.Description ?? string.Empty,
            Specials = card.Specials.Select(s => new CardSpecialOutputDTO { Name = s.Name }).ToList(),
            Rarity = new CardRarityOutputDTO { Name = card.Rarity.Name },
            Collection = new CardCollectionOutputDTO { Code = card.Collection.Code, Series = card.Collection.Series, Name = card.Collection.Name },
            CollectionNumber = card.CollectionNumber,
            Translations = card.Translations.Select(t => new CardTranslationOutputDTO
            {
                Id = t.Id,
                Culture = t.Culture,
                Name = t.Name,
                Description = t.Description ?? string.Empty
            }).ToList(),

            PokemonSpecials = pokemon.Specials.Select(s => new PokemonSpecialOutputDTO { Name = s.Name }).ToList(),
            Stage = new PokemonStageOutputDTO { Name = pokemon.Stage.Name },
            Hp = pokemon.Hp,
            PokemonType = new PokemonTypeOutputDTO { Id = pokemon.Type.Id, Name = pokemon.Type.Name },
            Weakness = pokemon.Weakness is null ? null : new PokemonTypeOutputDTO { Id = pokemon.Weakness.Id, Name = pokemon.Weakness.Name },
            RetreatCost = pokemon.RetreatCost,
            Ability = pokemon.Ability is null ? null : new PokemonAbilityOutputDTO { Id = pokemon.Ability.Id, Name = pokemon.Ability.Name },
            Attacks = pokemon.Attacks.Select(a => new PokemonAttackOutputDTO
            {
                Id = a.Id,
                Damage = a.Damage,
                Name = a.Name,
                Description = a.Description ?? string.Empty,
                Costs = a.Costs.Select(c => new PokemonTypeOutputDTO { Id = c.Id, Name = c.Name }).ToList()
            }).ToList()
        };
    }

    public static CardFossilOutputDTO ToFossilOutputDTO(this Card card, CardFossil fossil)
    {
        return new CardFossilOutputDTO
        {
            Id = card.Id,
            Type = new CardTypeOutputDTO { Name = card.Type.Name },
            Name = card.Name,
            Description = card.Description ?? string.Empty,
            Specials = card.Specials.Select(s => new CardSpecialOutputDTO { Name = s.Name }).ToList(),
            Rarity = new CardRarityOutputDTO { Name = card.Rarity.Name },
            Collection = new CardCollectionOutputDTO { Code = card.Collection.Code, Series = card.Collection.Series, Name = card.Collection.Name },
            CollectionNumber = card.CollectionNumber,
            Translations = card.Translations.Select(t => new CardTranslationOutputDTO
            {
                Id = t.Id,
                Culture = t.Culture,
                Name = t.Name,
                Description = t.Description ?? string.Empty
            }).ToList(),
            Hp = fossil.Hp
        };
    }

    public static CardItemOutputDTO ToItemOutputDTO(this Card card, CardItem item)
    {
        return new CardItemOutputDTO
        {
            Id = card.Id,
            Type = new CardTypeOutputDTO { Name = card.Type.Name },
            Name = card.Name,
            Description = card.Description ?? string.Empty,
            Specials = card.Specials.Select(s => new CardSpecialOutputDTO { Name = s.Name }).ToList(),
            Rarity = new CardRarityOutputDTO { Name = card.Rarity.Name },
            Collection = new CardCollectionOutputDTO { Code = card.Collection.Code, Series = card.Collection.Series, Name = card.Collection.Name },
            CollectionNumber = card.CollectionNumber,
            Translations = card.Translations.Select(t => new CardTranslationOutputDTO
            {
                Id = t.Id,
                Culture = t.Culture,
                Name = t.Name,
                Description = t.Description ?? string.Empty
            }).ToList(),
        };
    }

    public static CardSupporterOutputDTO ToSupporterOutputDTO(this Card card, CardSupporter supporter)
    {
        return new CardSupporterOutputDTO
        {
            Id = card.Id,
            Type = new CardTypeOutputDTO { Name = card.Type.Name },
            Name = card.Name,
            Description = card.Description ?? string.Empty,
            Specials = card.Specials.Select(s => new CardSpecialOutputDTO { Name = s.Name }).ToList(),
            Rarity = new CardRarityOutputDTO { Name = card.Rarity.Name },
            Collection = new CardCollectionOutputDTO { Code = card.Collection.Code, Series = card.Collection.Series, Name = card.Collection.Name },
            CollectionNumber = card.CollectionNumber,
            Translations = card.Translations.Select(t => new CardTranslationOutputDTO
            {
                Id = t.Id,
                Culture = t.Culture,
                Name = t.Name,
                Description = t.Description ?? string.Empty
            }).ToList(),
        };
    }

    public static CardToolOutputDTO ToToolOutputDTO(this Card card, CardTool tool)
    {
        return new CardToolOutputDTO
        {
            Id = card.Id,
            Type = new CardTypeOutputDTO { Name = card.Type.Name },
            Name = card.Name,
            Description = card.Description ?? string.Empty,
            Specials = card.Specials.Select(s => new CardSpecialOutputDTO { Name = s.Name }).ToList(),
            Rarity = new CardRarityOutputDTO { Name = card.Rarity.Name },
            Collection = new CardCollectionOutputDTO { Code = card.Collection.Code, Series = card.Collection.Series, Name = card.Collection.Name },
            CollectionNumber = card.CollectionNumber,
            Translations = card.Translations.Select(t => new CardTranslationOutputDTO
            {
                Id = t.Id,
                Culture = t.Culture,
                Name = t.Name,
                Description = t.Description ?? string.Empty
            }).ToList(),
        };
    }
}