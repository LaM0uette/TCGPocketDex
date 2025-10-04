namespace TCGPocketDex.Api.Entities;

public class CardItem
{
    public int CardId { get; set; }
    public required Card Card { get; set; }
}