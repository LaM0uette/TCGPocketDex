using TCGPocketDex.Contracts.DTO;

namespace TCGPocketDex.Api.Services;

public interface ICardService
{
    Task<CardPokemonOutputDTO> CreatePokemonAsync(CardPokemonInputDTO dto, CancellationToken ct = default);
    Task<CardFossilOutputDTO> CreateFossilAsync(CardFossilInputDTO dto, CancellationToken ct = default);
    Task<CardToolOutputDTO> CreateToolAsync(CardToolInputDTO dto, CancellationToken ct = default);
    Task<CardItemOutputDTO> CreateItemAsync(CardItemInputDTO dto, CancellationToken ct = default);
    Task<CardSupporterOutputDTO> CreateSupporterAsync(CardSupporterInputDTO dto, CancellationToken ct = default);
    Task<CardStadiumOutputDTO> CreateStadiumAsync(CardStadiumInputDTO dto, CancellationToken ct = default);

    Task<CardTranslationOutputDTO> AddCardTranslationAsync(int cardId, CardTranslationInputDTO dto, CancellationToken ct = default);
}
