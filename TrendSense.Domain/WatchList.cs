using System.Collections.Generic;

namespace TrendSense.Domain
{
    public class WatchList
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public AppUser User { get; set; } = null!;

        public string Name { get; set; } = "MyList";

        public ICollection<WatchListItem> Items { get; set; } = new List<WatchListItem>();
    }
}
