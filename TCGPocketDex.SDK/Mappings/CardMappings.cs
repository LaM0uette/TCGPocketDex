using TCGPocketDex.Contracts.DTO;
using TCGPocketDex.Domain.Models;

namespace TCGPocketDex.SDK.Mappings;

public static class CardMappings
{
    public static Card ToCard(this CardOutputDTO dto)
    {
        return new Card
        (
            dto.Id,
            dto.Type.ToCardType(),
            dto.Name,
            dto.Description ?? string.Empty,
            dto.Specials.ToCardSpecials(),
            dto.Rarity.ToCardRarity(),
            dto.Collection.ToCardCollection(),
            dto.CollectionNumber
        );
    }
    
    public static List<Card> ToCards(this List<CardOutputDTO> dtos)
    {
        return dtos.ConvertAll(dto => dto.ToCard());
    }
}