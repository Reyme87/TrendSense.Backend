using MediatR;
using TrendSense.Application.Common.Caching;
using TrendSense.Application.Interfaces;
using TrendSense.Domain;

namespace TrendSense.Application.Features.WatchLists.Commands.CreateWatchList
{
    public class CreateWatchListCommandHandler : IRequestHandler<CreateWatchListCommand, Guid>
    {
        private readonly IAppDbContext _dbContext;
        private readonly ICurrentUserService _currentUser;
        private readonly ICacheService _cache;

        public CreateWatchListCommandHandler(IAppDbContext dbContext, ICurrentUserService currentUser, ICacheService cache) =>
            (_dbContext, _currentUser, _cache) = (dbContext, currentUser, cache);

        public async Task<Guid> Handle(CreateWatchListCommand request, CancellationToken cancellationToken)
        {
            var watchList = new WatchList
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                UserId = _currentUser.UserId
            };

            await _dbContext.WatchLists.AddAsync(watchList, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            await _cache.RemoveAsync(CacheKeys.WatchLists(_currentUser.UserId), cancellationToken);

            return watchList.Id;
        }
    }
}
