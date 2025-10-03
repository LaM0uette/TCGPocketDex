using System.Net.Http.Headers;
using System.Text.Json;

namespace TCGPocketDex.SDK.Http;

public class ApiClient : IApiClient, IDisposable
{
    #region Statements

    private readonly HttpClient _http;
    private readonly JsonSerializerOptions _jsonOptions;

    public ApiClient(string jwtToken)
    {
        _http = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7057"), // TODO: Move to config
            Timeout = TimeSpan.FromSeconds(30)
        };
        
        _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
        _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    #endregion

    #region IApiClient

    public async Task<T> GetAsync<T>(string path, CancellationToken cancellationToken = default)
    {
        using HttpResponseMessage response = await _http.GetAsync(path, cancellationToken).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();
        
        string json = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
        Console.WriteLine(json);
        
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