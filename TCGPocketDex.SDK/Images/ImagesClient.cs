using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace TCGPocketDex.SDK.Images;

public class ImagesClient(HttpClient http) : IImagesClient
{
    private readonly HttpClient _http = http;

    public async Task<string[]> GetCardImageNamesAsync(CancellationToken ct = default)
        => await _http.GetFromJsonAsync<string[]>("images/cards", ct) ?? [];

    public string GetCardImageUrl(string name)
        => new Uri(_http.BaseAddress!, $"img/cards/{name}.webp").ToString();
}