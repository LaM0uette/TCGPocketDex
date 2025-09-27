using TCGPocketDex.Contracts.References;

namespace TCGPocketDex.Api.Services;

public interface IPokemonTypeService
{
    Task<IReadOnlyList<PokemonTypeOutputDTO>> GetAllAsync(string culture, CancellationToken ct);
    Task<PokemonTypeOutputDTO> CreateAsync(PokemonTypeInputDTO input, CancellationToken ct);
}
