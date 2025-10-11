using System.Linq;
using System.Collections.Generic;
using TCGPocketDex.Api.Entities;
using TCGPocketDex.Contracts.DTO;

namespace TCGPocketDex.Api.Mappings;

public static class CardTrainerMappings
{
    public static CardFossilOutputDTO ToFossilOutputDTO(this Card card, CardFossil fossil)
    {
        return new CardFossilOutputDTO(
            // card.Id,
            // new CardTypeOutputDTO(card.Type.Id, card.Type.Name),
            card.Name,
            // card.Description ?? string.Empty,
            null,
            // card.Specials.Select(s => new CardSpecialOutputDTO(s.Id, s.Name, [])).ToList(),
            // new CardRarityOutputDTO(card.Rarity.Id, card.Rarity.Name, []),
            //new CardCollectionOutputDTO(card.Collection.Id, card.Collection.Code, card.Collection.Series, card.Collection.Name),
            new CardCollectionOutputDTO(card.Collection.Code),
            card.CollectionNumber
            // fossil.Hp
        );
    }

    public static CardItemOutputDTO ToItemOutputDTO(this Card card, CardItem item)
    {
        return new CardItemOutputDTO(
            // card.Id,
            // new CardTypeOutputDTO(card.Type.Id, card.Type.Name),
            card.Name,
            // card.Description ?? string.Empty,
            null,
            // card.Specials.Select(s => new CardSpecialOutputDTO(s.Id, s.Name, [])).ToList(),
            // new CardRarityOutputDTO(card.Rarity.Id, card.Rarity.Name, []),
            //new CardCollectionOutputDTO(card.Collection.Id, card.Collection.Code, card.Collection.Series, card.Collection.Name),
            new CardCollectionOutputDTO(card.Collection.Code),
            card.CollectionNumber
        );
    }

    public static CardSupporterOutputDTO ToSupporterOutputDTO(this Card card, CardSupporter supporter)
    {
        return new CardSupporterOutputDTO(
            // card.Id,
            // new CardTypeOutputDTO(card.Type.Id, card.Type.Name),
            card.Name,
            // card.Description ?? string.Empty,
            null,
            // card.Specials.Select(s => new CardSpecialOutputDTO(s.Id, s.Name, [])).ToList(),
            // new CardRarityOutputDTO(card.Rarity.Id, card.Rarity.Name, []),
            //new CardCollectionOutputDTO(card.Collection.Id, card.Collection.Code, card.Collection.Series, card.Collection.Name),
            new CardCollectionOutputDTO(card.Collection.Code),
            card.CollectionNumber
        );
    }

    public static CardToolOutputDTO ToToolOutputDTO(this Card card, CardTool tool)
    {
        return new CardToolOutputDTO(
            // card.Id,
            // new CardTypeOutputDTO(card.Type.Id, card.Type.Name),
            card.Name,
            // card.Description ?? string.Empty,
            null,
            // card.Specials.Select(s => new CardSpecialOutputDTO(s.Id, s.Name, [])).ToList(),
            // new CardRarityOutputDTO(card.Rarity.Id, card.Rarity.Name, []),
            //new CardCollectionOutputDTO(card.Collection.Id, card.Collection.Code, card.Collection.Series, card.Collection.Name),
            new CardCollectionOutputDTO(card.Collection.Code),
            card.CollectionNumber
        );
    }
}