using TCGPocketDex.Contracts.DTO;
using TCGPocketDex.Domain.Models;

namespace TCGPocketDex.SDK.Mappings;

public static class CardRarityMappings
{
    public static CardRarity ToCardRarity(this CardRarityOutputDTO dto)
    {
        return new CardRarity(dto.Id, dto.Name);
    }
}