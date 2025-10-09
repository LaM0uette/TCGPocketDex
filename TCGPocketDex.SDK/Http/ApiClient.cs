using System.Text;
using System.Text.Json;

namespace TCGPocketDex.SDK.Http;

public class ApiClient : IApiClient, IDisposable
{
    #region Statements

    private readonly HttpClient _http;
    private readonly JsonSerializerOptions _jsonOptions;

    public ApiClient()
    {
        _http = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7057"), // TODO: Move to config
            Timeout = TimeSpan.FromSeconds(30)
        };

        _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    #endregion

    #region IApiClient

    public async Task<T> GetAsync<T>(string path, CancellationToken cancellationToken = default)
    {
        using HttpResponseMessage response = await _http.GetAsync(path, cancellationToken).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();
        
        string json = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
        T? deserialize = JsonSerializer.Deserialize<T>(json, _jsonOptions);
        return deserialize ?? throw new JsonException("Deserialization returned null.");
    }

    public async Task<T> PostAsync<T>(string path, object? body = null, CancellationToken cancellationToken = default)
    {
        using var content = body is null
            ? null
            : new StringContent(JsonSerializer.Serialize(body, _jsonOptions), Encoding.UTF8, "application/json");
        using HttpResponseMessage response = await _http.PostAsync(path, content, cancellationToken).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();

        string json = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
        T? deserialize = JsonSerializer.Deserialize<T>(json, _jsonOptions);
        return deserialize ?? throw new JsonException("Deserialization returned null.");
    }

    public async Task<T> GetAsync<T>(string path, object? body, CancellationToken cancellationToken = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, path)
        {
            Content = body is null
                ? null
                : new StringContent(JsonSerializer.Serialize(body, _jsonOptions), Encoding.UTF8, "application/json")
        };

        using HttpResponseMessage response = await _http.SendAsync(request, cancellationToken).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();

        string json = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
        T? deserialize = JsonSerializer.Deserialize<T>(json, _jsonOptions);
        return deserialize ?? throw new JsonException("Deserialization returned null.");
    }

    #endregion

    #region IDisposable

    public void Dispose()
    {
        _http.Dispose();
        
        GC.SuppressFinalize(this);
    }

    #endregion
}