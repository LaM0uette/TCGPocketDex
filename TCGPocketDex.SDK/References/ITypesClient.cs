using TCGPocketDex.Contracts.References;

namespace TCGPocketDex.SDK.References;

public interface ITypesClient
{
    Task<IReadOnlyList<PokemonTypeOutputDTO>> GetAllAsync(string culture, CancellationToken ct = default);
    Task<PokemonTypeOutputDTO?> CreateAsync(PokemonTypeInputDTO dto, CancellationToken ct = default);
}
