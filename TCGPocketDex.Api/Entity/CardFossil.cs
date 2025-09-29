namespace TCGPocketDex.Api.Entity;

public class CardFossil
{
    public int CardId { get; set; }
    public required Card Card { get; set; }
    
    public int Hp { get; set; }
}