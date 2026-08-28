using Microsoft.EntityFrameworkCore;
using TrendSense.Domain;

namespace TrendSense.Application.Interfaces
{
    public interface IAppDbContext 
    {
        //DbSet<AppUser> Users { get; set; }
        DbSet<Stock> Stocks { get; set; }
        DbSet<WatchList> WatchLists { get; set; }
        DbSet<WatchListItem> Items { get; set; }
        DbSet<PriceHistory> History { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
