using System.ComponentModel.DataAnnotations;

namespace TCGPocketDex.Api.Entity;

public class Card
{
    public int Id { get; init; }
    
    [MaxLength(100)]
    public required string Name { get; set; }
    
    public bool IsEx { get; set; }
    
    public bool IsMega { get; set; }
    
    public required PokemonStage Stage { get; set; }
    
    public required PokemonType Type { get; set; }
    
    public required int Hp { get; set; }
    
    [MaxLength(255)]
    public required string ImageUrl { get; set; }
    
    public required PokemonWeakness Weakness { get; set; }
    
    public int RetreatCost { get; set; }
    
    public required CardRarity Rarity { get; set; }
    
    /*public ICollection<CardAbility> Abilities { get; init; } = [];
    
    public ICollection<CardAttack> Attacks { get; init; } = [];*/
    
}