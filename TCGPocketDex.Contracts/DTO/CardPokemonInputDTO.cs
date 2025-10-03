namespace TCGPocketDex.Contracts.DTO;

public class CardPokemonInputDTO
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    
    public bool IsPromo { get; set; }
    public required int CardRarityId { get; set; }
    public int? CardSetId { get; set; }
    public int? SerieNumber { get; set; }
    
    public int PokemonSpecials { get; set; }
    public int Stage { get; set; }
    public int Hp { get; set; }

    public required int TypeId { get; set; }
    public int? WeaknessTypeId { get; set; }

    public int RetreatCost { get; set; }

    public int? PokemonAbilityId { get; set; }
    public List<PokemonAttackInputDTO> Attacks { get; set; } = [];
}