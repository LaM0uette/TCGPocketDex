namespace TCGPocketDex.Domain.Models;

public record CardTranslation(
    int Id,
    Card Card,
    string Culture,
    string Name,
    string Description
);