﻿using TCGPocketDex.Contracts.Request;
using TCGPocketDex.Domain.Models;

namespace TCGPocketDex.SDK.Services;

public interface ICardService
{
    Task<List<Card>> GetAllAsync(string? cultureOverride = null, CancellationToken ct = default);
    Task<Card?> GetByIdAsync(int id, string? cultureOverride = null, CancellationToken ct = default);
    Task<List<Card>> GetByBatchAsync(DeckRequest deck, string? cultureOverride = null, CancellationToken ct = default);
}