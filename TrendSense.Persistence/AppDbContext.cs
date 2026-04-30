using Microsoft.EntityFrameworkCore;
using TrendSense.Application.Interfaces;
using TrendSense.Domain;
using TrendSense.Persistence.EntityTypeConfiguration;

namespace TrendSense.Persistence
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<WatchList> WatchLists { get; set; }
        public DbSet<WatchListItem> Items { get; set; }
        public DbSet<PriceHistory> History { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            modelBuilder.ApplyConfiguration(new StockConfiguration());
            modelBuilder.ApplyConfiguration(new WatchListConfiguration());
            modelBuilder.ApplyConfiguration(new WatchListItemConfiguration());
            modelBuilder.ApplyConfiguration(new PriceHistoryConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
