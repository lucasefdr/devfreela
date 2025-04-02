using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using DevFreela.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace DevFreela.Infrastructure.Auth;

public class TokenService(IConfiguration configuration) : ITokenService
{
    public string GenerateJwtToken(string email, string userName, string role)
    {
        // Criação das claims
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, email),
            new Claim(ClaimTypes.Name, userName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, role)
        };
        
        // Cria uma chave simétrica para encriptar a chave registrada
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));

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

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string ComputePasswordHash(string password)
    {
        // Encripta a senha para salvar no banco
        var passwordBytes = Encoding.UTF8.GetBytes(password);
        var hashBytes = SHA256.HashData(passwordBytes);

        return Convert.ToBase64String(hashBytes);
    }
}