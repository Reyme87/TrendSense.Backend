namespace TrendSense.Application.Dtos
{
    public class StockMarketInfo
    {
        public string SecId { get; set; } = null!;
        public string BoardId { get; set; } = null!;
        public string ShortName { get; set; } = null!;
        public string Isin { get; set; } = null!;
        public string CurrencyId { get; set; } = null!;

        public double? Last { get; set; }
        public double? Change { get; set; }
        public double? ChangePercent { get; set; }
        public double? Open { get; set; }
        public double? Close { get; set; }
        public double? Low { get; set; }
        public double? High { get; set; }

        public string TradingStatus { get; set; } = null!;
        public DateTime? Time { get; set; }
    }
}
