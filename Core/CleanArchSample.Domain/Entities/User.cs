using Microsoft.AspNetCore.Identity;

namespace CleanArchSample.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string FullName { get; set; }
        public string? RefreshToken { get; set; }
        public DateTimeOffset? RefreshTokenExpiry { get; set; }
    }
}
