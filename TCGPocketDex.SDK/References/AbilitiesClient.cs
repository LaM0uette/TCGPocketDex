using System.Net.Http.Json;
using TCGPocketDex.Contracts.References;

namespace TCGPocketDex.SDK.References;

public class AbilitiesClient(HttpClient http) : IAbilitiesClient
{
    public async Task<IReadOnlyList<PokemonAbilityOutputDTO>> GetAllAsync(string culture, CancellationToken ct = default)
    {
        var res = await http.GetFromJsonAsync<IReadOnlyList<PokemonAbilityOutputDTO>>($"pokemon-abilities?culture={Uri.EscapeDataString(culture)}", ct);
        return res ?? Array.Empty<PokemonAbilityOutputDTO>();
    }

    public async Task<PokemonAbilityOutputDTO?> CreateAsync(PokemonAbilityInputDTO dto, CancellationToken ct = default)
    {
        var res = await http.PostAsJsonAsync("pokemon-abilities", dto, ct);
        if (!res.IsSuccessStatusCode) return null;
        return await res.Content.ReadFromJsonAsync<PokemonAbilityOutputDTO>(cancellationToken: ct);
    }
}
