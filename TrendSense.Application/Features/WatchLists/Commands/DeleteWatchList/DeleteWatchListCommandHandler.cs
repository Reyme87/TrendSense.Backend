using MediatR;
using Microsoft.EntityFrameworkCore;
using TrendSense.Application.Common.Caching;
using TrendSense.Application.Common.Exceptions;
using TrendSense.Application.Interfaces;
using TrendSense.Domain;

namespace TrendSense.Application.Features.WatchLists.Commands.DeleteWatchList
{
    public class DeleteWatchListCommandHandler : IRequestHandler<DeleteWatchListCommand, Unit>
    {
        private readonly IAppDbContext _dbContext;
        private readonly ICurrentUserService _currentUser;
        private readonly ICacheService _cache;

        public DeleteWatchListCommandHandler(IAppDbContext dbContext, ICurrentUserService currentUser, ICacheService cache) => 
            (_dbContext, _currentUser, _cache) = (dbContext, currentUser, cache);

        public async Task<Unit> Handle(DeleteWatchListCommand request, CancellationToken cancellationToken)
        {
            var watchlist = await _dbContext.WatchLists
            .FirstOrDefaultAsync(x =>
                x.Id == request.WatchListId &&
                x.UserId == _currentUser.UserId,
                cancellationToken);

            if (watchlist == null)
            {
                throw new NotFoundException(nameof(WatchList), request.WatchListId);
            }

            _dbContext.WatchLists.Remove(watchlist);
            await _dbContext.SaveChangesAsync(cancellationToken);

            await _cache.RemoveAsync(CacheKeys.WatchLists(_currentUser.UserId), cancellationToken);

            return Unit.Value;
        }
    }
}
