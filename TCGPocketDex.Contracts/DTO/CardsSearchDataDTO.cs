using System.Collections.Generic;

namespace TCGPocketDex.Contracts.DTO;

public record CardsSearchDataDTO(
    IReadOnlyList<string> Pokemon,
    IReadOnlyList<string> Fossil,
    IReadOnlyList<string> Item,
    IReadOnlyList<string> Tool,
    IReadOnlyList<string> Supporter,
    IReadOnlyList<string> Stadium
);