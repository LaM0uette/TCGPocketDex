namespace TCGPocketDex.Domain.Models;

public record CardSetTranslation(
    int Id,
    CardSet CardSet,
    string Culture,
    string Name
);