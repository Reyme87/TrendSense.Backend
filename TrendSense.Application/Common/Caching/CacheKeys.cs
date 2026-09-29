namespace TrendSense.Application.Common.Caching
{
    public static class CacheKeys
    {
        public const string Stocks = "stocks:all";

        public static string WatchLists(Guid userId) => $"watchlists:{userId}";

        public static string PriceHistory(Guid stockId) => $"stocks:{stockId}:history";
    }
}
