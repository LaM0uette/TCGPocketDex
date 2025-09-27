using System.Net.Http.Json;
using TCGPocketDex.Contracts.References;

namespace TCGPocketDex.SDK.References;

public class StagesClient(HttpClient http) : IStagesClient
{
    public async Task<IReadOnlyList<PokemonStageOutputDTO>> GetAllAsync(string culture, CancellationToken ct = default)
    {
        var res = await http.GetFromJsonAsync<IReadOnlyList<PokemonStageOutputDTO>>($"pokemon-stages?culture={Uri.EscapeDataString(culture)}", ct);
        return res ?? Array.Empty<PokemonStageOutputDTO>();
    }

    public async Task<PokemonStageOutputDTO?> CreateAsync(PokemonStageInputDTO dto, CancellationToken ct = default)
    {
        var res = await http.PostAsJsonAsync("pokemon-stages", dto, ct);
        if (!res.IsSuccessStatusCode) return null;
        return await res.Content.ReadFromJsonAsync<PokemonStageOutputDTO>(cancellationToken: ct);
    }
}
