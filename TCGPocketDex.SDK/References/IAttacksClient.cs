using TCGPocketDex.Contracts.References;

namespace TCGPocketDex.SDK.References;

public interface IAttacksClient
{
    Task<IReadOnlyList<PokemonAttackOutputDTO>> GetAllAsync(string culture, CancellationToken ct = default);
    Task<PokemonAttackOutputDTO?> CreateAsync(PokemonAttackInputDTO dto, CancellationToken ct = default);
}
