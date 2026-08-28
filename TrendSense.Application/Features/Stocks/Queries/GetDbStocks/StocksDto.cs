namespace TrendSense.Application.Features.Stocks.Queries.GetDbStocks
{
    public class StockDto
    {
        public Guid Id { get; set; }

        public string TickerSymbol { get; set; } = null!;
        public string Name { get; set; } = null!;

        public string Exchange { get; set; } = null!;

        public double? LastPrice { get; set; }
               
        public double? DayChange { get; set; }
        public double? DayChangePercent { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
