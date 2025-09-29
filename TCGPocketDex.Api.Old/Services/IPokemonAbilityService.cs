using TCGPocketDex.Contracts.References;

namespace TCGPocketDex.Api.Old.Services;

public interface IPokemonAbilityService
{
    Task<IReadOnlyList<PokemonAbilityOutputDTO>> GetAllAsync(string culture, CancellationToken ct);
    Task<PokemonAbilityOutputDTO> CreateAsync(PokemonAbilityInputDTO input, CancellationToken ct);
}
