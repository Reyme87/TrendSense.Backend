using MediatR;
using Microsoft.EntityFrameworkCore;
using TrendSense.Application.Interfaces;

namespace TrendSense.Application.Features.Stocks.Queries.GetDbStocks
{
    public class GetDbStocksQueryHandler : IRequestHandler<GetDbStocksQuery, IReadOnlyList<StockDto>>
    {
        private const string CacheKey = "stocks:all";
        private static readonly TimeSpan CacheExpiration = TimeSpan.FromMinutes(1);
        private readonly IAppDbContext _dbContext;
        private readonly ICacheService _cache;

        public GetDbStocksQueryHandler(IAppDbContext dbContext, ICacheService cache) => (_dbContext, _cache) = (dbContext, cache);

        public async Task<IReadOnlyList<StockDto>> Handle(GetDbStocksQuery request, CancellationToken cancellationToken)
        {
            var cached = await _cache.GetAsync<IReadOnlyList<StockDto>>(CacheKey, cancellationToken);

            if (cached is not null)
            {
                return cached;
            }

            var stocks = await _dbContext.Stocks
                .AsNoTracking()
                .Select(stock => new StockDto
                {
                    Id = stock.Id,
                    TickerSymbol = stock.TickerSymbol,
                    Name = stock.Name,
                    Exchange = stock.Exchange,
                    LastPrice = stock.LastPrice,
                    DayChange = stock.DayChange,
                    DayChangePercent = stock.DayChangePercent,
                    UpdatedAt = stock.UpdatedAt
                })
                .ToListAsync(cancellationToken);

            await _cache.SetAsync(CacheKey, stocks, CacheExpiration, cancellationToken);

            return stocks;
        }
    }
}
