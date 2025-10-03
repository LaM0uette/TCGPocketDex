namespace TCGPocketDex.SDK.Http;

public interface IApiClient
{
    Task<T> GetAsync<T>(string path, CancellationToken cancellationToken = default);
}