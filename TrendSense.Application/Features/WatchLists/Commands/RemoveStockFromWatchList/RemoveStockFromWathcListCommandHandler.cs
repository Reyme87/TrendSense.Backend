using MediatR;
using Microsoft.EntityFrameworkCore;
using TrendSense.Application.Common.Caching;
using TrendSense.Application.Common.Exceptions;
using TrendSense.Application.Interfaces;
using TrendSense.Domain;

namespace TrendSense.Application.Features.WatchLists.Commands.RemoveStockFromWatchList
{
    public class RemoveStockFromWathcListCommandHandler : IRequestHandler<RemoveStockFromWatchListCommand, Unit>
    {
        private readonly IAppDbContext _dbContext;
        private readonly ICurrentUserService _currentUser;
        private readonly ICacheService _cache;

        public RemoveStockFromWathcListCommandHandler(IAppDbContext dbContext, ICurrentUserService currentUser, ICacheService cache) =>
            (_dbContext, _currentUser, _cache) = (dbContext, currentUser, cache);

        public async Task<Unit> Handle(RemoveStockFromWatchListCommand request, CancellationToken cancellationToken)
        {
            var item = await _dbContext.Items
                .Include(x => x.WatchList)
                .FirstOrDefaultAsync(x => x.WatchListId == request.WatchListId && x.StockId == request.StockId && x.WatchList.UserId == _currentUser.UserId, cancellationToken);

            if (item is null)
            {
                return Unit.Value;
            }

            _dbContext.Items.Remove(item);
            await _dbContext.SaveChangesAsync(cancellationToken);

            await _cache.RemoveAsync(CacheKeys.WatchLists(_currentUser.UserId), cancellationToken);

            return Unit.Value;
        }
    }
}
