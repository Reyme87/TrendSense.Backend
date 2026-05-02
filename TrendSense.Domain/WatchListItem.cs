namespace TrendSense.Domain
{
    public class WatchListItem
    {
        public Guid Id { get; set; }
        public Guid WatchListId { get; set; }
        public WatchList WatchList { get; set; } = null!;
        public Guid StockId { get; set; }

        public Stock Stock { get; set; }

        public DateTime AddedAt { get; set; }
    }
}
