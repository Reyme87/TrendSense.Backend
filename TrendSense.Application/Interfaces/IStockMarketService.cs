using TrendSense.Application.Dtos;

namespace TrendSense.Application.Interfaces
{
    public interface IStockMarketService
    {
        public Task<IReadOnlyCollection<StockMarketDataDto>> GetStocksAsync(CancellationToken cancellationToken);

        public Task<StockMarketDataDto> GetStockAsync(string ticker, CancellationToken cancellationToken);
    }
}
