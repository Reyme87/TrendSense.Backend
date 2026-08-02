using System.Text.Json;
using TrendSense.Application.Dtos;
using TrendSense.Application.Interfaces;
using TrendSense.Infrastructure.Moex.Dtos;

namespace TrendSense.Infrastructure.Moex
{
    public class MoexStockMarketService : IStockMarketService
    {
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        private readonly HttpClient _httpClient;

        public MoexStockMarketService(HttpClient httpClient) => _httpClient = httpClient;

        public async Task<StockMarketInfo?> GetStockAsync(string secId, CancellationToken cancellationToken)
        {
            var response = await GetMoexResponseAsync(secId, cancellationToken);

            if (response?.Securities is null || response.Securities.Data.Count == 0 ||
                response.MarketData is null || response.MarketData.Data.Count == 0)
            {
                return null;
            }

            var secRow = CreateRow(response.Securities, response.Securities.Data[0]);
            var mdRow = CreateRow(response.MarketData, response.MarketData.Data[0]);

            var security = new MoexSecurityDto
            {
                SecId = GetString(secRow, "SECID")!,
                BoardId = GetString(secRow, "BOARDID")!,
                ShortName = GetString(secRow, "SHORTNAME")!,
                Isin = GetString(secRow, "ISIN")!,
                CurrencyId = GetString(secRow, "CURRENCYID")!
            };

            var marketData = new MoexMarketDataDto
            {
                SecId = GetString(mdRow, "SECID")!,
                BoardId = GetString(mdRow, "BOARDID")!,
                Last = GetDouble(mdRow, "LAST"),
                Change = GetDouble(mdRow, "CHANGE"),
                ChangePercent = GetDouble(mdRow, "LASTCHANGEPRCNT"),
                Open = GetDouble(mdRow, "OPEN"),
                Close = GetDouble(mdRow, "CLOSE"),
                Low = GetDouble(mdRow, "LOW"),
                High = GetDouble(mdRow, "HIGH"),
                TradingStatus = GetString(mdRow, "TRADINGSTATUS") ?? string.Empty,
                SysTime = GetString(mdRow, "SYSTIME")
            };

            DateTime? time = null;
            if (marketData.SysTime is not null &&
                DateTime.TryParse(marketData.SysTime, out var parsedTime))
            {
                time = parsedTime;
            }

            return new StockMarketInfo
            {
                SecId = security.SecId,
                BoardId = security.BoardId,
                ShortName = security.ShortName,
                Isin = security.Isin,
                CurrencyId = security.CurrencyId,
                Last = marketData.Last,
                Change = marketData.Change,
                ChangePercent = marketData.ChangePercent,
                Open = marketData.Open,
                Close = marketData.Close,
                Low = marketData.Low,
                High = marketData.High,
                TradingStatus = marketData.TradingStatus,
                Time = time
            };
        }

        private async Task<MoexMarketDataResponse?> GetMoexResponseAsync(string secId, CancellationToken cancellationToken)
        {
            var url = $"engines/stock/markets/shares/boards/TQBR/securities/{secId}.json";

            using var response = await _httpClient.GetAsync(url, cancellationToken);

            response.EnsureSuccessStatusCode();

            await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);

            return await JsonSerializer.DeserializeAsync<MoexMarketDataResponse>(stream, JsonOptions, cancellationToken);
        }

        private static Dictionary<string, JsonElement> CreateRow(MoexBlock block, List<JsonElement> row)
        {
            return block.Columns
                .Select((column, index) => new { column, index })
                .ToDictionary(
                    x => x.column,
                    x => row[x.index]);
        }

        private static string? GetString(Dictionary<string, JsonElement> row, string column)
        {
            if (!row.TryGetValue(column, out var element) || element.ValueKind == JsonValueKind.Null)
                return null;

            return element.GetString();
        }

        private static double? GetDouble(Dictionary<string, JsonElement> row, string column)
        {
            if (!row.TryGetValue(column, out var element) || element.ValueKind == JsonValueKind.Null)
                return null;

            return element.GetDouble();
        }
    }
}
