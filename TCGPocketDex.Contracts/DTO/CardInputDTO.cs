namespace TCGPocketDex.Contracts.DTO;

public class CardInputDTO
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    
    public List<int> CardSpecialIds { get; set; } = [];
    public required int CardRarityId { get; set; }
    public int CardCollectionId { get; set; }
    public int CollectionNumber { get; set; }
}