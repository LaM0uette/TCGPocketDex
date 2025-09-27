using System.Net.Http.Json;
using TCGPocketDex.Contracts.Cards;

namespace TCGPocketDex.SDK.Cards;

public class CardsClient(HttpClient http) : ICardsClient
{
    public async Task<IReadOnlyList<CardOutputDTO>> GetAllAsync(string culture, CancellationToken ct = default)
    {
        var url = $"cards?culture={Uri.EscapeDataString(culture)}";
        var result = await http.GetFromJsonAsync<IReadOnlyList<CardOutputDTO>>(url, ct);
        return result ?? Array.Empty<CardOutputDTO>();
    }

    public Task<CardOutputDTO?> GetByIdAsync(int id, string culture, CancellationToken ct = default)
    {
        var url = $"cards/{id}?culture={Uri.EscapeDataString(culture)}";
        return http.GetFromJsonAsync<CardOutputDTO?>(url, ct);
    }

    public async Task<CardOutputDTO> CreateAsync(CardInputDTO input, CancellationToken ct = default)
    {
        var resp = await http.PostAsJsonAsync("cards", input, ct);
        resp.EnsureSuccessStatusCode();
        var created = await resp.Content.ReadFromJsonAsync<CardOutputDTO>(cancellationToken: ct) ?? throw new InvalidOperationException("No content");
        return created;
    }

    public async Task<CardOutputDTO?> UpdateAsync(int id, CardInputDTO input, CancellationToken ct = default)
    {
        var resp = await http.PutAsJsonAsync($"cards/{id}", input, ct);
        if (!resp.IsSuccessStatusCode) return null;
        var updated = await resp.Content.ReadFromJsonAsync<CardOutputDTO>(cancellationToken: ct);
        return updated;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
    {
        var resp = await http.DeleteAsync($"cards/{id}", ct);
        return resp.IsSuccessStatusCode;
    }
}
