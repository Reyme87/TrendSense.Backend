namespace TrendSense.Infrastructure.Moex.Dtos
{
    public class MoexMarketDataDto
    {
        public string SecId { get; set; } = null!;
        public string BoardId { get; set; } = null!;
        public double? Last { get; set; }
        public double? Change { get; set; }
        public double? ChangePercent { get; set; }
        public double? Open { get; set; }
        public double? Close { get; set; }
        public double? Low { get; set; }
        public double? High { get; set; }
        public string TradingStatus { get; set; } = null!;
        public string? SysTime { get; set; }
    }
}
