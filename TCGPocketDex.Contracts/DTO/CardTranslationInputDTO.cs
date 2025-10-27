namespace TCGPocketDex.Contracts.DTO;

public record CardTranslationInputDTO(
    string Culture,
    string Name,
    string? Description
);