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
            // card.Id,
            new CardTypeOutputDTO(card.Type.Id, card.Type.Name),
            card.Name,
            // card.Description ?? string.Empty,
            null,
            // card.Specials.Select(s => new CardSpecialOutputDTO(s.Id, s.Name, [])).ToList(),
            // new CardRarityOutputDTO(card.Rarity.Id, card.Rarity.Name, []),
            //new CardCollectionOutputDTO(card.Collection.Id, card.Collection.Code, card.Collection.Series, card.Collection.Name),
            new CardCollectionOutputDTO(card.Collection.Code),
            card.CollectionNumber,
            pokemon.Specials.Select(s => new PokemonSpecialOutputDTO(s.Id, s.Name)).ToList(),
            // new PokemonStageOutputDTO(pokemon.Stage.Id, pokemon.Stage.Name),
            // pokemon.Hp,
            new PokemonTypeOutputDTO(pokemon.Type.Id, pokemon.Type.Name)
            // pokemon.Weakness is null ? null : new PokemonTypeOutputDTO(pokemon.Weakness.Id, pokemon.Weakness.Name),
            // pokemon.RetreatCost,
            // pokemon.Ability is null ? null : new PokemonAbilityOutputDTO(pokemon.Ability.Id, pokemon.Ability.Name, null),
            // pokemon.Attacks.Select(a => new PokemonAttackOutputDTO(
            //     a.Id,
            //     a.Damage,
            //     a.Name,
            //     a.Description,
            //     a.Costs.Select(c => new PokemonTypeOutputDTO(c.Id, c.Name)).ToList()
            // )).ToList()
        );
    }
}