using TCGPocketDex.Api.Old.Repositories;
using TCGPocketDex.Contracts.References;

namespace TCGPocketDex.Api.Old.Services;

public class PokemonTypeService(IPokemonTypeRepository repo) : IPokemonTypeService
{
    public Task<IReadOnlyList<PokemonTypeOutputDTO>> GetAllAsync(string culture, CancellationToken ct) => repo.GetAllAsync(culture, ct);

    public Task<PokemonTypeOutputDTO> CreateAsync(PokemonTypeInputDTO input, CancellationToken ct) => repo.CreateAsync(input, ct);
}
