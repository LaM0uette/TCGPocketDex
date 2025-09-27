using System.Net.Http.Json;
using TCGPocketDex.Contracts.References;

namespace TCGPocketDex.SDK.References;

public class RaritiesClient(HttpClient http) : IRaritiesClient
{
    public async Task<IReadOnlyList<CardRarityOutputDTO>> GetAllAsync(CancellationToken ct = default)
    {
        var res = await http.GetFromJsonAsync<IReadOnlyList<CardRarityOutputDTO>>("card-rarities", ct);
        return res ?? Array.Empty<CardRarityOutputDTO>();
    }

    public async Task<CardRarityOutputDTO?> CreateAsync(CardRarityInputDTO dto, CancellationToken ct = default)
    {
        var res = await http.PostAsJsonAsync("card-rarities", dto, ct);
        if (!res.IsSuccessStatusCode) return null;
        return await res.Content.ReadFromJsonAsync<CardRarityOutputDTO>(cancellationToken: ct);
    }
}
