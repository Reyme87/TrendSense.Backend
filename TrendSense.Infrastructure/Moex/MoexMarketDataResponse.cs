using System.Text.Json.Serialization;

namespace TrendSense.Infrastructure.Moex
{
    public class MoexMarketDataResponse
    {
        [JsonPropertyName("securities")]
        public MoexBlock? Securities { get; set; }

        [JsonPropertyName("marketdata")]
        public MoexBlock MarketData { get; set; } = null!;
    }
}
