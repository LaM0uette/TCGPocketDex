using TCGPocketDex.Api.Entities;
using TCGPocketDex.Contracts.DTO;

namespace TCGPocketDex.Api.Mappings;

public static class CardMappings
{
    public static CardOutputDTO ToOutputDTO(this Card card)
    {
        return new CardOutputDTO
        {
            Id = card.Id,
            CardTypeId = card.CardTypeId,
            Name = card.Name,
            Description = card.Description ?? string.Empty,
            CardSpecialIds = card.Specials.Select(s => s.Id).ToList(),
            CardRarityId = card.CardRarityId,
            CardSetId = card.CardCollectionId,
            SerieNumber = card.CollectionNumber
        };
    }
}