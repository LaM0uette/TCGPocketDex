using System.Net.Http.Json;
using TCGPocketDex.Contracts.References;

namespace TCGPocketDex.SDK.References;

public class BoostersClient(HttpClient http) : IBoostersClient
{
    public async Task<IReadOnlyList<BoosterOutputDTO>> GetAllAsync(string culture, int? cardExtensionId = null, CancellationToken ct = default)
    {
        var url = $"boosters?culture={Uri.EscapeDataString(culture)}" + (cardExtensionId.HasValue ? $"&cardExtensionId={cardExtensionId.Value}" : string.Empty);
        var res = await http.GetFromJsonAsync<IReadOnlyList<BoosterOutputDTO>>(url, ct);
        return res ?? Array.Empty<BoosterOutputDTO>();
    }

    public async Task<BoosterOutputDTO?> CreateAsync(BoosterInputDTO dto, CancellationToken ct = default)
    {
        var res = await http.PostAsJsonAsync("boosters", dto, ct);
        if (!res.IsSuccessStatusCode) return null;
        return await res.Content.ReadFromJsonAsync<BoosterOutputDTO>(cancellationToken: ct);
    }
}
