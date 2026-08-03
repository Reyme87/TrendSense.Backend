namespace TrendSense.Domain
{
    public class Stock
    {
        public Guid Id { get; set; }

        public string TickerSymbol { get; set; } = null!;
        public string Name { get; set; } = null!;

        public string Isin { get; set; } = null!;
        public string Currency { get; set; } = null!;

        public string Exchange { get; set; } = null!;
        public string Sector { get; set; } = null!;

        public decimal LastPrice { get; set; }

        public decimal DayChange { get; set; }
        public decimal DayChangePercent { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
