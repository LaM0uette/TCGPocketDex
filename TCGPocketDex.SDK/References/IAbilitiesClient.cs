using TCGPocketDex.Contracts.References;

namespace TCGPocketDex.SDK.References;

public interface IAbilitiesClient
{
    Task<IReadOnlyList<PokemonAbilityOutputDTO>> GetAllAsync(string culture, CancellationToken ct = default);
    Task<PokemonAbilityOutputDTO?> CreateAsync(PokemonAbilityInputDTO dto, CancellationToken ct = default);
}
