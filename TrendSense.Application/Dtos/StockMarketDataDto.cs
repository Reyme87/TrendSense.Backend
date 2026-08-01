namespace TrendSense.Application.Dtos
{
    public class StockMarketDataDto
    {
        public string TickerSymbol { get; set; } = null!;
        public string Name { get; set; } = null!;
        public decimal LastPrice { get; set; }
        public decimal PreviousPrice { get; set; }

    }
}
