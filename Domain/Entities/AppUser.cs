using Microsoft.AspNetCore.Identity;

namespace MoviesAPI.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime RefreshTokenExpiryTime { get; set; }
    }

}
