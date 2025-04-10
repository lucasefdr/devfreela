using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace DevFreela.Infrastructure.Auth;

public class TokenService(IConfiguration configuration) : ITokenService
{
    public string GenerateJwtToken(IEnumerable<Claim> claims)
    {
        // Cria uma chave simétrica para encriptar a chave registrada
        var key = GetSymmetricSecurityKey();

        // Registra as credenciais
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // Configura o tempo de expiração do token
        var expiresIn = DateTime.Now.AddMinutes(Convert.ToDouble(configuration["Jwt:DurationInMinutes"]));

        // Realiza a criação do token
        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            signingCredentials: creds,
            expires: expiresIn);
        
        var handler = new JwtSecurityTokenHandler();    
        
        return handler.WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        var secureRandomBytes = new byte[128];

        using var randomNumberGenerator = RandomNumberGenerator.Create();

        randomNumberGenerator.GetBytes(secureRandomBytes);

        var refreshToken = Convert.ToBase64String(secureRandomBytes);
        return refreshToken;
    }

    public ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
    {
        var key = GetSymmetricSecurityKey();

        var tokenValidation = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key,
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidation, out var securityToken);

        if (securityToken is not JwtSecurityToken)
        {
            throw new SecurityTokenException("Invalid token");
        }

        return principal;
    }

    private SymmetricSecurityKey GetSymmetricSecurityKey()
        => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
}