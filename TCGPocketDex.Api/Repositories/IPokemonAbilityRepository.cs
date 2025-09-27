using TCGPocketDex.Contracts.References;

namespace TCGPocketDex.Api.Repositories;

public interface IPokemonAbilityRepository
{
    Task<IReadOnlyList<PokemonAbilityOutputDTO>> GetAllAsync(string culture, CancellationToken ct);
    Task<PokemonAbilityOutputDTO> CreateAsync(PokemonAbilityInputDTO input, CancellationToken ct);
}
