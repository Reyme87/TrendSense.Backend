using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TrendSense.Application.Interfaces;

namespace TrendSense.Application.Features.WatchLists.Queries.GetWatchLists
{
    public class GetWatchListsQueryHandler : IRequestHandler<GetWatchListsQuery, WatchListVm>
    {
        private IAppDbContext _dbContext;
        private IMapper _mapper;
        private ICurrentUserService _currentUserService;

        public GetWatchListsQueryHandler(IAppDbContext dbContext, IMapper mapper, ICurrentUserService currentUserService) =>
            (_dbContext, _mapper, _currentUserService) = (dbContext, mapper, currentUserService);

        public async Task<WatchListVm> Handle(GetWatchListsQuery request, CancellationToken cancellationToken)
        {
            var listsQuery = await _dbContext.WatchLists
                .Where(x => x.UserId == _currentUserService.UserId)
                .ProjectTo<WatchListLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new WatchListVm { WatchLists = listsQuery };
        }
    }
}
