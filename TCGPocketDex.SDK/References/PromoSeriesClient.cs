using System.Net.Http.Json;
using TCGPocketDex.Contracts.References;

namespace TCGPocketDex.SDK.References;

public class PromoSeriesClient(HttpClient http) : IPromoSeriesClient
{
    public async Task<IReadOnlyList<PromoSeriesOutputDTO>> GetAllAsync(string culture, CancellationToken ct = default)
    {
        var res = await http.GetFromJsonAsync<IReadOnlyList<PromoSeriesOutputDTO>>($"promo-series?culture={Uri.EscapeDataString(culture)}", ct);
        return res ?? Array.Empty<PromoSeriesOutputDTO>();
    }

    public async Task<PromoSeriesOutputDTO?> CreateAsync(PromoSeriesInputDTO dto, CancellationToken ct = default)
    {
        var res = await http.PostAsJsonAsync("promo-series", dto, ct);
        if (!res.IsSuccessStatusCode) return null;
        return await res.Content.ReadFromJsonAsync<PromoSeriesOutputDTO>(cancellationToken: ct);
    }
}
