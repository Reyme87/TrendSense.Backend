using MediatR;
using Microsoft.EntityFrameworkCore;
using TrendSense.Application.Interfaces;

namespace TrendSense.Application.Features.Stocks.Queries.GetDbStocks
{
    public class GetDbStocksQueryHandler : IRequestHandler<GetDbStocksQuery, IReadOnlyList<StockDto>>
    {
        public IAppDbContext _dbContext;

        public GetDbStocksQueryHandler(IAppDbContext dbContext) => _dbContext = dbContext;

        public async Task<IReadOnlyList<StockDto>> Handle(GetDbStocksQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Stocks
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
        }
    }
}
