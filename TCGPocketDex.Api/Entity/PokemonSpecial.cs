namespace TCGPocketDex.Api.Entity;

[Flags]
public enum PokemonSpecial
{
    None = 0,
    Ex = 1 << 0,
    Mega = 1 << 1,
}
