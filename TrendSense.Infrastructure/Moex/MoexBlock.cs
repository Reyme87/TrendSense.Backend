using System.Text.Json;
using System.Text.Json.Serialization;

namespace TrendSense.Infrastructure.Moex
{
    public class MoexBlock
    {
        [JsonPropertyName("columns")]
        public List<string> Columns { get; set; } = [];

        [JsonPropertyName("data")]
        public List<List<JsonElement>> Data { get; set; } = [];
    }
}
