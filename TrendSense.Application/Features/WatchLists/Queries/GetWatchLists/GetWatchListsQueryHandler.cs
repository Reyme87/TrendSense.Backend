using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TrendSense.Application.Common.Caching;
using TrendSense.Application.Interfaces;

namespace TrendSense.Application.Features.WatchLists.Queries.GetWatchLists
{
    public class GetWatchListsQueryHandler : IRequestHandler<GetWatchListsQuery, WatchListVm>
    {
        private static readonly TimeSpan CacheExpiration = TimeSpan.FromMinutes(5);

        private IAppDbContext _dbContext;
        private IMapper _mapper;
        private ICurrentUserService _currentUserService;
        private ICacheService _cache;

        public GetWatchListsQueryHandler(IAppDbContext dbContext, IMapper mapper, ICurrentUserService currentUserService, ICacheService cache) =>
            (_dbContext, _mapper, _currentUserService, _cache) = (dbContext, mapper, currentUserService, cache);

        public async Task<WatchListVm> Handle(GetWatchListsQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            var cacheKey = CacheKeys.WatchLists(userId);

            var cached = await _cache.GetAsync<List<WatchListLookupDto>>(cacheKey, cancellationToken);

            if (cached is not null)
            {
                return new WatchListVm
                {
                    WatchLists = cached
                };
            }

            var listsQuery = await _dbContext.WatchLists
                .Where(x => x.UserId == _currentUserService.UserId)
                .ProjectTo<WatchListLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            await _cache.SetAsync(cacheKey, listsQuery, CacheExpiration, cancellationToken);

            return new WatchListVm { WatchLists = listsQuery };
        }
    }
}
