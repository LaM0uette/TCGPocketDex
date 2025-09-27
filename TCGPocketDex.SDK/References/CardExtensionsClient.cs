using System.Net.Http.Json;
using TCGPocketDex.Contracts.References;

namespace TCGPocketDex.SDK.References;

public class CardExtensionsClient(HttpClient http) : ICardExtensionsClient
{
    public async Task<IReadOnlyList<CardExtensionOutputDTO>> GetAllAsync(string culture, CancellationToken ct = default)
    {
        var res = await http.GetFromJsonAsync<IReadOnlyList<CardExtensionOutputDTO>>($"card-extensions?culture={Uri.EscapeDataString(culture)}", ct);
        return res ?? Array.Empty<CardExtensionOutputDTO>();
    }

    public async Task<CardExtensionOutputDTO?> CreateAsync(CardExtensionInputDTO dto, CancellationToken ct = default)
    {
        var res = await http.PostAsJsonAsync("card-extensions", dto, ct);
        if (!res.IsSuccessStatusCode) return null;
        return await res.Content.ReadFromJsonAsync<CardExtensionOutputDTO>(cancellationToken: ct);
    }
}
