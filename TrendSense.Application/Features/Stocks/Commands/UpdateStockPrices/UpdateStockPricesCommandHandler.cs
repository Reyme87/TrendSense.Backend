using MediatR;
using TrendSense.Application.Common.Caching;
using TrendSense.Application.Interfaces;
using TrendSense.Domain;

namespace TrendSense.Application.Features.Stocks.Commands.UpdateStockPrices
{
    public class UpdateStockPricesCommandHandler : IRequestHandler<UpdateStockPricesCommand, Unit>
    {
        private readonly IAppDbContext _dbContext;
        private readonly ICacheService _cache;
        private readonly IStockMarketService _stockMarketService;

        public UpdateStockPricesCommandHandler(IAppDbContext dbContext, IStockMarketService stockMarketService, ICacheService cache) => 
            (_dbContext, _stockMarketService, _cache) = (dbContext, stockMarketService, cache);

        public async Task<Unit> Handle(UpdateStockPricesCommand request, CancellationToken cancellationToken)
        {
            var marketStocks = await _stockMarketService.GetStocksListAsync(cancellationToken);

            var stocks = _dbContext.Stocks.ToList();

            var marketStocksByTicker = marketStocks.Where(x => x is not null).ToDictionary(x => x!.SecId);

            var updatedStockIds = new HashSet<Guid>();

            foreach(var stock in stocks)
            {
                if(!marketStocksByTicker.TryGetValue(stock.TickerSymbol, out var marketStock))
                {
                    continue;
                }

                stock.LastPrice = marketStock!.Last ?? stock.LastPrice;
                stock.DayChange = marketStock.Change ?? stock.DayChange;
                stock.DayChangePercent = marketStock.ChangePercent ?? stock.DayChangePercent;

                updatedStockIds.Add(stock.Id);

                stock.UpdatedAt = marketStock.Time ?? DateTime.Now;

                if (marketStock.Last.HasValue)
                {
                    _dbContext.History.Add(new PriceHistory
                    {
                        Id = Guid.NewGuid(),
                        StockId = stock.Id,
                        Price = marketStock.Last.Value,
                        RecordedAt = marketStock.Time ?? DateTime.UtcNow
                    });
                }
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            await _cache.RemoveAsync(CacheKeys.Stocks, cancellationToken);

            foreach (var stockId in updatedStockIds)
            {
                await _cache.RemoveAsync(CacheKeys.PriceHistory(stockId), cancellationToken);
            }

            return Unit.Value;
        }
    }
}
