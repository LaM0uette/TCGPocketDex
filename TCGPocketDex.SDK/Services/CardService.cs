using System.Net;
using TCGPocketDex.SDK.Http;
using TCGPocketDex.SDK.Models;

namespace TCGPocketDex.SDK.Services;

public class CardService : ICardService
{
    #region Statements

    private readonly IApiClient _apiClient;

    public CardService(IApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    #endregion

    #region ICardService

    public Task<List<Card>> GetAllAsync(CancellationToken ct = default)
    {
        return _apiClient.GetAsync<List<Card>>("/cards", ct);
    }

    public async Task<Card?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        try
        {
            return await _apiClient.GetAsync<Card>($"/cards/{id}", ct).ConfigureAwait(false);
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    #endregion
}