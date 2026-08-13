using MediatR;
using System.Data.Entity;
using TrendSense.Application.Interfaces;
using TrendSense.Domain;

namespace TrendSense.Application.Features.Stocks.Commands.UpdateStockPrices
{
    public class UpdateStockPricesCommandHandler : IRequestHandler<UpdateStockPricesCommand, Unit>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IStockMarketService _stockMarketService;

        public UpdateStockPricesCommandHandler(IAppDbContext dbContext, IStockMarketService stockMarketService) => 
            (_dbContext, _stockMarketService) = (dbContext, stockMarketService);

        public async Task<Unit> Handle(UpdateStockPricesCommand request, CancellationToken cancellationToken)
        {
            var marketStocks = await _stockMarketService.GetStocksListAsync(cancellationToken);

            var stocks = _dbContext.Stocks.ToList();

            var marketStocksByTicker = marketStocks.Where(x => x is not null).ToDictionary(x => x!.SecId);

            foreach(var stock in stocks)
            {
                if(!marketStocksByTicker.TryGetValue(stock.TickerSymbol, out var marketStock))
                {
                    continue;
                }

                stock.LastPrice = marketStock!.Last ?? stock.LastPrice;
                stock.DayChange = marketStock.Change ?? stock.DayChange;
                stock.DayChangePercent = marketStock.ChangePercent ?? stock.DayChangePercent;

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

            return Unit.Value;
        }
    }
}
