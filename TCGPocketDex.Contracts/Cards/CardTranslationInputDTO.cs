namespace TCGPocketDex.Contracts.Cards;

public record CardTranslationInputDTO(
    string Culture,
    string Name,
    string? Description,
    string? ImageUrl
);
