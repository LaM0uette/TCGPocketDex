using TCGPocketDex.Contracts.DTO;
using TCGPocketDex.Domain.Models;

namespace TCGPocketDex.SDK.Mappings;

public static class CardCollectionMappings
{
    public static CardCollection ToCardCollection(this CardCollectionOutputDTO dto)
    {
        return new CardCollection(dto.Id, dto.Code, dto.Series, dto.Name);
    }
}