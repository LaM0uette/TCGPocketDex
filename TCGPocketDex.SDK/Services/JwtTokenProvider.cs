using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace TCGPocketDex.SDK.Services;

public interface IJwtTokenProvider
{
    string GetToken();
}

public class JwtTokenProvider : IJwtTokenProvider
{
    private readonly string _issuer;
    private readonly string _audience;
    private readonly TimeSpan _lifetime;
    private readonly RsaSecurityKey _privateKey;

    public JwtTokenProvider(
        string? privateKeyPem = null,
        string? issuer = null,
        string? audience = null,
        TimeSpan? lifetime = null)
    {
        privateKeyPem ??= Environment.GetEnvironmentVariable("TCGPDK_JWT_PRIVATE_KEY_PEM");
        if (string.IsNullOrWhiteSpace(privateKeyPem))
        {
            throw new InvalidOperationException("TCGPDK_JWT_PRIVATE_KEY_PEM is not configured.");
        }

        RSA rsa = RSA.Create();
        rsa.ImportFromPem(privateKeyPem);
        _privateKey = new RsaSecurityKey(rsa);

        _issuer = issuer ?? Environment.GetEnvironmentVariable("TCGPDK_JWT_ISSUER") ?? "TCGPocketDex.SDK";
        _audience = audience ?? Environment.GetEnvironmentVariable("TCGPDK_JWT_AUDIENCE") ?? "TCGPocketDex.Api";

        if (!int.TryParse(Environment.GetEnvironmentVariable("TCGPDK_JWT_LIFETIME_MINUTES"), out int minutes))
        {
            minutes = 5; // short lived token by default
        }
        _lifetime = lifetime ?? TimeSpan.FromMinutes(minutes);
    }

    public string GetToken()
    {
        DateTimeOffset now = DateTimeOffset.UtcNow;
        ClaimsIdentity identity = new([
            new Claim(JwtRegisteredClaimNames.Sub, "sdk"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
            new Claim(JwtRegisteredClaimNames.Iat, now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
        ]);

        SigningCredentials creds = new(_privateKey, SecurityAlgorithms.RsaSha256);

        JwtSecurityToken token = new(
            issuer: _issuer,
            audience: _audience,
            claims: identity.Claims,
            notBefore: now.UtcDateTime,
            expires: now.Add(_lifetime).UtcDateTime,
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
