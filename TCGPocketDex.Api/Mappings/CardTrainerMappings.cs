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
            new CardTypeOutputDTO(card.Type?.Id ?? card.CardTypeId, card.Type?.Name ?? string.Empty),
            card.Name,
            null,
            new CardCollectionOutputDTO(card.Collection?.Code ?? string.Empty),
            card.CollectionNumber
        );
    }

    public static CardItemOutputDTO ToItemOutputDTO(this Card card, CardItem item)
    {
        return new CardItemOutputDTO(
            new CardTypeOutputDTO(card.Type?.Id ?? card.CardTypeId, card.Type?.Name ?? string.Empty),
            card.Name,
            null,
            new CardCollectionOutputDTO(card.Collection?.Code ?? string.Empty),
            card.CollectionNumber
        );
    }

    public static CardSupporterOutputDTO ToSupporterOutputDTO(this Card card, CardSupporter supporter)
    {
        return new CardSupporterOutputDTO(
            new CardTypeOutputDTO(card.Type?.Id ?? card.CardTypeId, card.Type?.Name ?? string.Empty),
            card.Name,
            null,
            new CardCollectionOutputDTO(card.Collection?.Code ?? string.Empty),
            card.CollectionNumber
        );
    }

    public static CardToolOutputDTO ToToolOutputDTO(this Card card, CardTool tool)
    {
        return new CardToolOutputDTO(
            new CardTypeOutputDTO(card.Type?.Id ?? card.CardTypeId, card.Type?.Name ?? string.Empty),
            card.Name,
            null,
            new CardCollectionOutputDTO(card.Collection?.Code ?? string.Empty),
            card.CollectionNumber
        );
    }

    public static CardStadiumOutputDTO ToStadiumOutputDTO(this Card card, CardStadium stadium)
    {
        return new CardStadiumOutputDTO(
            new CardTypeOutputDTO(card.Type?.Id ?? card.CardTypeId, card.Type?.Name ?? string.Empty),
            card.Name,
            null,
            new CardCollectionOutputDTO(card.Collection?.Code ?? string.Empty),
            card.CollectionNumber
        );
    }
}