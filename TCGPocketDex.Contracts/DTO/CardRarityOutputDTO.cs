namespace TCGPocketDex.Contracts.DTO;

public record CardRarityOutputDTO(
    int Id,
    string Name,
    List<CardRarityTranslationOutputDTO> Translations
);
