using MediatR;
using TrendSense.Application.Features.Stocks.Commands.SyncStocks;
using TrendSense.Application.Interfaces;
using TrendSense.Domain;

namespace TrendSense.Application.Features.Stocks.Commands
{
    public class SyncStocksCommandHandler : IRequestHandler<SyncStocksCommand, Unit>
    {
        private readonly IAppDbContext _dbContext;

        private readonly IStockMarketService _stockMarketService;

        public SyncStocksCommandHandler(IAppDbContext dbContext, IStockMarketService stockMarketService) =>
            (_dbContext, _stockMarketService) = (dbContext, stockMarketService);

        public async Task<Unit> Handle(SyncStocksCommand request, CancellationToken cancellationToken)
        {
            var marketStocks = await _stockMarketService.GetStocksListAsync(cancellationToken);

            foreach (var marketStock in marketStocks)
            {
                if (marketStock is null)
                {
                    continue;
                }

                //var stock = await _dbContext.Stocks.FirstOrDefaultAsync(s => s.TickerSymbol == marketStock.SecId, cancellationToken);
                var stock = _dbContext.Stocks.FirstOrDefault(x => x.TickerSymbol == marketStock.SecId);

                if (stock is null)
                {
                    stock = new Stock
                    {
                        Id = Guid.NewGuid(),
                        TickerSymbol = marketStock.SecId,
                        Name = marketStock.ShortName,
                        Isin = marketStock.Isin,
                        Currency = marketStock.CurrencyId,
                        Exchange = "MOEX",
                        LastPrice = marketStock.Last,
                        DayChange = marketStock.Change,
                        DayChangePercent = marketStock.ChangePercent,
                        UpdatedAt = marketStock.Time ?? DateTime.Now
                    };

                    _dbContext.Stocks.Add(stock);
                }
                else
                {
                    stock.Name = marketStock.ShortName;
                    stock.LastPrice = marketStock.Last;
                    stock.DayChange = marketStock.Change;
                    stock.DayChangePercent = marketStock.ChangePercent;
                    stock.UpdatedAt = marketStock.Time ?? DateTime.Now;
                }
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
