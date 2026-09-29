using MediatR;
using Microsoft.EntityFrameworkCore;
using TrendSense.Application.Common.Caching;
using TrendSense.Application.Interfaces;

namespace TrendSense.Application.Features.Stocks.Queries.GetPriceHistory
{
    public class GetPriceHistoryQueryHandler : IRequestHandler<GetPriceHistoryQuery, IReadOnlyList<PriceHistoryDto>>
    {
        private static readonly TimeSpan CacheExpiration = TimeSpan.FromMinutes(1);
        private readonly IAppDbContext _dbContext;
        private readonly ICacheService _cache;

        public GetPriceHistoryQueryHandler(IAppDbContext dbContext, ICacheService cache) => (_dbContext, _cache) = (dbContext, cache);

        public async Task<IReadOnlyList<PriceHistoryDto>> Handle(GetPriceHistoryQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = CacheKeys.PriceHistory(request.StockId);

            var cached = await _cache.GetAsync<List<PriceHistoryDto>>(cacheKey, cancellationToken);

            if (cached is not null)
            {
                return cached;
            }

            var history = await _dbContext.History.AsNoTracking()
                                   .Where(x => x.StockId == request.StockId)
                                   .OrderBy(x => x.RecordedAt)
                                   .Select(x => new PriceHistoryDto
                                   {
                                       Price = x.Price,
                                       RecoredAt = x.RecordedAt,
                                   })
                                   .ToListAsync(cancellationToken);

            await _cache.SetAsync(cacheKey, history, CacheExpiration, cancellationToken);

            return history;
        }
    }
}
