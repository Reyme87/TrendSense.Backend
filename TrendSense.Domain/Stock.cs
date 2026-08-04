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

        public double? LastPrice { get; set; }
        public double? DayChange { get; set; }
        public double? DayChangePercent { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
