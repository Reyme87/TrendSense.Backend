using TrendSense.Application.Dtos;

namespace TrendSense.Application.Interfaces
{
    public interface IStockMarketService
    {
        public Task<StockMarketInfo?> GetStockAsync(string secId, CancellationToken cancellationToken);

        public Task<IReadOnlyList<StockMarketInfo?>> GetStocksListAsync(CancellationToken cancellationToken);
    }
}
