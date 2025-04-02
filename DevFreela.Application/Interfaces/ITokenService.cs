namespace DevFreela.Application.Interfaces;

public interface ITokenService
{
    string GenerateJwtToken(string email, string userName, string role);
    string ComputePasswordHash(string password);
}