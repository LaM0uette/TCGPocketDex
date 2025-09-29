namespace TCGPocketDex.Api.Entity;

public class CardItem
{
    public int CardId { get; set; }
    public required Card Card { get; set; }
}