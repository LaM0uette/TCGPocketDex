namespace TCGPocketDex.Contracts.DTO;

public class CardPokemonInputDTO : CardInputDTO
{
    public List<int> PokemonSpecialIds { get; set; } = [];
    public int PokemonStageId { get; set; }
    public int Hp { get; set; }

    public required int PokemonTypeId { get; set; }
    public int? WeaknessPokemonTypeId { get; set; }

    public int RetreatCost { get; set; }

    public int? PokemonAbilityId { get; set; }
    public List<PokemonAttackInputDTO> Attacks { get; set; } = [];
}