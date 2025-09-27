using System.Net.Http.Json;
using TCGPocketDex.Contracts.References;

namespace TCGPocketDex.SDK.References;

public class TypesClient(HttpClient http) : ITypesClient
{
    public async Task<IReadOnlyList<PokemonTypeOutputDTO>> GetAllAsync(string culture, CancellationToken ct = default)
    {
        var res = await http.GetFromJsonAsync<IReadOnlyList<PokemonTypeOutputDTO>>($"pokemon-types?culture={Uri.EscapeDataString(culture)}", ct);
        return res ?? Array.Empty<PokemonTypeOutputDTO>();
    }

    public async Task<PokemonTypeOutputDTO?> CreateAsync(PokemonTypeInputDTO dto, CancellationToken ct = default)
    {
        var res = await http.PostAsJsonAsync("pokemon-types", dto, ct);
        if (!res.IsSuccessStatusCode) return null;
        return await res.Content.ReadFromJsonAsync<PokemonTypeOutputDTO>(cancellationToken: ct);
    }
}
