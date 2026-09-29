using MediatR;
using Microsoft.EntityFrameworkCore;
using TrendSense.Application.Common.Caching;
using TrendSense.Application.Common.Exceptions;
using TrendSense.Application.Interfaces;
using TrendSense.Domain;

namespace TrendSense.Application.Features.WatchLists.Commands.AddStockToWatchList
{
    public class AddStockToWatchListCommandHandler : IRequestHandler<AddStockToWatchListCommand, Unit>
    {
        private readonly IAppDbContext _dbContext;
        private readonly ICurrentUserService _currentUser;
        private readonly ICacheService _cache;

        public AddStockToWatchListCommandHandler(IAppDbContext dbContext, ICurrentUserService currentUser, ICacheService cache) =>
            (_dbContext, _currentUser, _cache) = (dbContext, currentUser, cache);

        public async Task<Unit> Handle(AddStockToWatchListCommand request, CancellationToken cancellationToken)
        {
            var watchListExists = await _dbContext.WatchLists
                .AnyAsync(x => x.Id == request.WatchListId && x.UserId == _currentUser.UserId, cancellationToken);

            if (!watchListExists)
            {
                throw new NotFoundException(nameof(WatchList), request.WatchListId);
            }

            var alreadyExists = await _dbContext.Items
                .AnyAsync(x => x.WatchListId == request.WatchListId && x.StockId == request.StockId, cancellationToken);

            if (alreadyExists)
            {
                return Unit.Value;
            }

            var newItem = new WatchListItem
            {
                Id = Guid.NewGuid(),
                StockId = request.StockId,
                WatchListId = request.WatchListId,
                AddedAt = DateTime.UtcNow
            };

            _dbContext.Items.Add(newItem);
            await _dbContext.SaveChangesAsync(cancellationToken);

            await _cache.RemoveAsync(CacheKeys.WatchLists(_currentUser.UserId), cancellationToken);

            return Unit.Value;
        }
    }
}
