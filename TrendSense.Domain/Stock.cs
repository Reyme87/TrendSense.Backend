namespace TrendSense.Domain
{
    public class Stock
    {
        public Guid Id { get; set; }

        public string TickerSymbol { get; set; }
        public string Name { get; set; }
        
        public string Exchange { get; set; }
        public string Sector { get; set; }

        public decimal LastPrice { get; set; }

        public decimal DayChange { get; set; }
        public decimal DayChangePercent { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
