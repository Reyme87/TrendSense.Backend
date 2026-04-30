namespace TrendSense.Domain
{
    public class PriceHistory
    {
        public Guid Id { get; set; }

        public Guid StockId { get; set; }
        public Stock Stock { get; set; }

        public decimal Price { get; set; }

        public DateTime RecordedAt { get; set; }
    }
}
