namespace TCGPocketDex.Api.Entity;

[Flags]
public enum CardSpecial
{
    None = 0,
    Promo = 1 << 0,
}
