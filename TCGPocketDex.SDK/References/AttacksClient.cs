using System.Net.Http.Json;
using TCGPocketDex.Contracts.References;

namespace TCGPocketDex.SDK.References;

public class AttacksClient(HttpClient http) : IAttacksClient
{
    public async Task<IReadOnlyList<PokemonAttackOutputDTO>> GetAllAsync(string culture, CancellationToken ct = default)
    {
        var res = await http.GetFromJsonAsync<IReadOnlyList<PokemonAttackOutputDTO>>($"pokemon-attacks?culture={Uri.EscapeDataString(culture)}", ct);
        return res ?? Array.Empty<PokemonAttackOutputDTO>();
    }

    public async Task<PokemonAttackOutputDTO?> CreateAsync(PokemonAttackInputDTO dto, CancellationToken ct = default)
    {
        var res = await http.PostAsJsonAsync("pokemon-attacks", dto, ct);
        if (!res.IsSuccessStatusCode) return null;
        return await res.Content.ReadFromJsonAsync<PokemonAttackOutputDTO>(cancellationToken: ct);
    }
}
