using System.Net.Http.Headers;
using System.Text.Json;
using TCGPocketDex.SDK.Services;

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
        
        // Automatically generate and attach a short-lived JWT signed by SDK's RSA private key when available.
        try
        {
            IJwtTokenProvider tokenProvider = new JwtTokenProvider();
            string jwtToken = tokenProvider.GetToken();
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
        }
        catch
        {
            // In browser (Blazor WASM) we won't have the private key. Proceed without Authorization for public endpoints.
        }

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