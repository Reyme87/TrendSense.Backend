using MediatR;
using TrendSense.Application.Interfaces;
using TrendSense.Domain;

namespace TrendSense.Application.Features.WatchLists.Commands.CreateWatchList
{
    public class CreateWatchListCommandHandler : IRequestHandler<CreateWatchListCommand, Guid>
    {
        private readonly IAppDbContext _dbContext;
        private readonly ICurrentUserService _currentUser;

        public CreateWatchListCommandHandler(IAppDbContext dbContext, ICurrentUserService currentUser) =>
            (_dbContext, _currentUser) = (dbContext, currentUser);

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

            return watchList.Id;
        }
    }
}
