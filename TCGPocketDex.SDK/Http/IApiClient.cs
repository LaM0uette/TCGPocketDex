namespace TCGPocketDex.SDK.Http;

public interface IApiClient
{
    Task<T> GetAsync<T>(string path, CancellationToken cancellationToken = default);
    Task<T> GetAsync<T>(string path, object? body, CancellationToken cancellationToken = default);
    Task<T> PostAsync<T>(string path, object? body = null, CancellationToken cancellationToken = default);
}