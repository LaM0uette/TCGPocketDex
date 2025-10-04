using System.Net;
using System.Globalization;
using TCGPocketDex.Contracts.DTO;
using TCGPocketDex.Domain.Models;
using TCGPocketDex.SDK.Http;
using TCGPocketDex.SDK.Mappings;

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

    public async Task<List<Card>> GetAllAsync(string? lngOverride = null, CancellationToken ct = default)
    {
        string culture = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
        string query = string.IsNullOrWhiteSpace(lngOverride)
            ? $"?culture={culture}"
            : $"?lng={NormalizeLng(lngOverride)}";
        List<CardOutputDTO> dtos = await _apiClient.GetAsync<List<CardOutputDTO>>($"/cards{query}", ct);
        List<Card> cards = dtos.ToCards();
        
        return cards;
    }

    public async Task<Card?> GetByIdAsync(int id, string? lngOverride = null, CancellationToken ct = default)
    {
        string culture = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
        string query = string.IsNullOrWhiteSpace(lngOverride)
            ? $"?culture={culture}"
            : $"?lng={NormalizeLng(lngOverride)}";
        try
        {
            // Keep existing behavior but pass query based on override/culture.
            return await _apiClient.GetAsync<Card>($"/cards/{id}{query}", ct).ConfigureAwait(false);
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    private static string NormalizeLng(string value)
    {
        var v = value?.Trim();
        if (string.IsNullOrEmpty(v)) return "";
        return v.Length >= 2 ? v[..2].ToLowerInvariant() : v.ToLowerInvariant();
    }

    #endregion
}