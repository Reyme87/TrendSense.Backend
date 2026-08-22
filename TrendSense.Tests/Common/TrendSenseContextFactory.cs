using Microsoft.EntityFrameworkCore;
using TrendSense.Domain;
using TrendSense.Persistence;

namespace TrendSense.Tests.Common
{
    public static class TrendSenseContextFactory
    {
        public static readonly Guid UserAId = Guid.NewGuid();

        public static readonly Guid UserBId = Guid.NewGuid();

        public static readonly Guid StockSberId = Guid.NewGuid();

        public static readonly Guid StockGazpromId = Guid.NewGuid();

        public static readonly Guid WatchListAId = Guid.NewGuid();

        public static readonly Guid WatchListBId = Guid.NewGuid();

        public static readonly Guid WatchListItemAId = Guid.NewGuid();

        public static AppDbContext Create()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new AppDbContext(options);

            context.Database.EnsureCreated();

            Seed(context);

            return context;
        }

        private static void Seed(AppDbContext context)
        {
            var userA = new AppUser
            {
                Id = UserAId,
                UserName = "userA",
                Email = "userA@test.com"
            };

            var userB = new AppUser
            {
                Id = UserBId,
                UserName = "userB",
                Email = "userB@test.com"
            };

            context.Users.AddRange(userA, userB);

            var sber = new Stock
            {
                Id = StockSberId,
                TickerSymbol = "SBER",
                Name = "Сбербанк",
                Exchange = "MOEX",
                Currency = "SUR",
                Isin = "12345678901",
                LastPrice = 300,
                DayChange = 5,
                DayChangePercent = 1.67,
                UpdatedAt = DateTime.UtcNow
            };

            var gazprom = new Stock
            {
                Id = StockGazpromId,
                TickerSymbol = "GAZP",
                Name = "Газпром",
                Exchange = "MOEX",
                Currency = "SUR",
                Isin = "12345678902",
                LastPrice = 150,
                DayChange = -2,
                DayChangePercent = -1.33,
                UpdatedAt = DateTime.UtcNow
            };

            context.Stocks.AddRange(sber, gazprom);

            var watchListA = new WatchList
            {
                Id = WatchListAId,
                UserId = UserAId,
                Name = "User A List"
            };

            var watchListB = new WatchList
            {
                Id = WatchListBId,
                UserId = UserBId,
                Name = "User B List"
            };

            context.WatchLists.AddRange(watchListA, watchListB);

            var watchListItemA = new WatchListItem
            {
                Id = WatchListItemAId,
                WatchListId = WatchListAId,
                StockId = StockSberId,
                AddedAt = DateTime.UtcNow
            };

            context.Items.Add(watchListItemA);

            context.SaveChanges();
        }


        public static void Destroy(AppDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}