namespace TCGPocketDex.Contracts.DTO;

public record CardSpecialOutputDTO(
    int Id,
    string Name,
    List<CardSpecialTranslationOutputDTO> Translation
);
