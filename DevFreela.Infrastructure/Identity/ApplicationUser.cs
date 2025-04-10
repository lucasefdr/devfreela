using Microsoft.AspNetCore.Identity;

namespace DevFreela.Infrastructure.Identity;

public class ApplicationUser : IdentityUser<int>
{
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    public DateTime CreatedAt { get; set; }
}