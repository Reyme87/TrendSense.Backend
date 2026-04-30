using System.Collections;

namespace TrendSense.Domain
{
    public class AppUser
    {
        public Guid Id { get; set; }

        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public ICollection<WatchList> Collection { get; set; }
    }
}
