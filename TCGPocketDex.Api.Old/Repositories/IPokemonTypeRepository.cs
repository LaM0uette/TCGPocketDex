using TCGPocketDex.Contracts.References;

namespace TCGPocketDex.Api.Old.Repositories;

public interface IPokemonTypeRepository
{
    Task<IReadOnlyList<PokemonTypeOutputDTO>> GetAllAsync(string culture, CancellationToken ct);
    Task<PokemonTypeOutputDTO> CreateAsync(PokemonTypeInputDTO input, CancellationToken ct);
}
