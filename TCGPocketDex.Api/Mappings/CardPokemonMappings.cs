using System.Linq;
using System.Collections.Generic;
using TCGPocketDex.Api.Entities;
using TCGPocketDex.Contracts.DTO;

namespace TCGPocketDex.Api.Mappings;

public static class CardPokemonMappings
{
    public static CardPokemonOutputDTO ToPokemonOutputDTO(this Card card, CardPokemon pokemon)
    {
        return new CardPokemonOutputDTO(
            new CardTypeOutputDTO(card.Type?.Id ?? card.CardTypeId, card.Type?.Name ?? string.Empty),
            card.Name,
            null,
            new CardCollectionOutputDTO(card.Collection?.Code ?? string.Empty),
            card.CollectionNumber,
            pokemon.Specials.Select(s => new PokemonSpecialOutputDTO(s.Id, s.Name)).ToList(),
            new PokemonTypeOutputDTO(pokemon.Type?.Id ?? pokemon.PokemonTypeId, pokemon.Type?.Name ?? string.Empty)
        );
    }
}