using TCGPocketDex.Contracts.DTO;
using TCGPocketDex.Domain.Models;

namespace TCGPocketDex.SDK.Mappings;

public static class CardTypeMappings
{
    public static CardType ToCardType(this CardTypeOutputDTO dto)
    {
        return new CardType(dto.Id, dto.Name);
    }
}