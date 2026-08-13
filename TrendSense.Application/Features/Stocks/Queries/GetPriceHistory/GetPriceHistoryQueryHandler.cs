using MediatR;
using System.Data.Entity;
using TrendSense.Application.Interfaces;

namespace TrendSense.Application.Features.Stocks.Queries.GetPriceHistory
{
    public class GetPriceHistoryQueryHandler : IRequestHandler<GetPriceHistoryQuery, IReadOnlyList<PriceHistoryDto>>
    {
        private readonly IAppDbContext _dbContext;

        public GetPriceHistoryQueryHandler(IAppDbContext dbContext) => _dbContext = dbContext;

        public async Task<IReadOnlyList<PriceHistoryDto>> Handle(GetPriceHistoryQuery request, CancellationToken cancellationToken)
        {
            return _dbContext.History.AsNoTracking()
                                   .Where(x => x.StockId == request.StockId)
                                   .OrderBy(x => x.RecordedAt)
                                   .Select(x => new PriceHistoryDto
                                   {
                                       Price = x.Price,
                                       RecoredAt = x.RecordedAt,
                                   })
                                   .ToList();
        }
    }
}
