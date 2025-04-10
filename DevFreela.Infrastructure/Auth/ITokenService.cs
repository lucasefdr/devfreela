using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DevFreela.Infrastructure.Auth;

public interface ITokenService
{
    string GenerateJwtToken(IEnumerable<Claim> claims);
    string GenerateRefreshToken();
    ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
    
}