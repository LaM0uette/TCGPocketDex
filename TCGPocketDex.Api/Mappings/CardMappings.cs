using TCGPocketDex.Api.Entities;
using TCGPocketDex.Contracts.DTO;

namespace TCGPocketDex.Api.Mappings;

public static class CardMappings
{
    public static List<CardOutputDTO> ToDTOs(this IEnumerable<Card> cards, string culture = "en")
    {
        return cards.Select(c => c.ToDTO(culture)).ToList();
    }
    
    public static CardOutputDTO ToDTO(this Card card, string culture = "en")
    {
        CardTranslation? cardTranslation = card.Translations.FirstOrDefault(ct => string.Equals(ct.Culture, culture));
        CardTypeTranslation? cardTypeTranslation = card.Type.Translations.FirstOrDefault(ctt => string.Equals(ctt.Culture, culture));
        CardRarityTranslation? cardRarityTranslation = card.Rarity.Translations.FirstOrDefault(crt => string.Equals(crt.Culture, culture));
        CardCollectionTranslation? cardCollectionTranslation = card.Collection.Translations.FirstOrDefault(cct => string.Equals(cct.Culture, culture));
        
        int id = card.Id;
        CardTypeOutputDTO type = new(card.Type.Id, cardTypeTranslation?.Name ?? card.Type.Name);
        string name = cardTranslation?.Name ?? card.Name;
        string description = cardTranslation?.Description ?? card.Description ?? string.Empty;
        string imageUrl = $"https://tcgp-dex.com/cards/{culture}/{card.Collection.Code}-{card.CollectionNumber}.webp"; // full path example: https://tcgp-dex.com/cards/en/A1-1.webp
        List<CardSpecialOutputDTO> specials = card.GetSpecialsWithCulture(culture);
        CardRarityOutputDTO rarity = new(card.Rarity.Id, cardRarityTranslation?.Name ?? card.Rarity.Name, []);
        CardCollectionOutputDTO collection = new(card.Collection.Id, card.Collection.Code, card.Collection.Series, cardCollectionTranslation?.Name ?? card.Collection.Name);
        int collectionNumber = card.CollectionNumber;
        
        return new CardOutputDTO(id, type, name, description, imageUrl, specials, rarity, collection, collectionNumber);
    }
    
    
    private static List<CardSpecialOutputDTO> GetSpecialsWithCulture(this Card card, string culture)
    {
        return card.Specials.Select(cs => new CardSpecialOutputDTO(
            cs.Id,
            cs.Translations.FirstOrDefault(cst => cst.Culture == culture)?.Name ?? cs.Name,
            []
        )).ToList();
    }
    
    
    
    

    #region ToRemove // TODO: Remove

    // Methods moved to dedicated mapping classes: CardPokemonMappings and CardTrainerMappings

    #endregion
}