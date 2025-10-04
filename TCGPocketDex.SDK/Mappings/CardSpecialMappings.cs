using TCGPocketDex.Contracts.DTO;
using TCGPocketDex.Domain.Models;

namespace TCGPocketDex.SDK.Mappings;

public static class CardSpecialMappings
{
    public static CardSpecial ToCardSpecial(this CardSpecialOutputDTO dto)
    {
        return new CardSpecial(dto.Id, dto.Name);
    }
    
    public static List<CardSpecial> ToCardSpecials(this IEnumerable<CardSpecialOutputDTO> dtos)
    {
        return dtos.Select(dto => dto.ToCardSpecial()).ToList();
    }
}