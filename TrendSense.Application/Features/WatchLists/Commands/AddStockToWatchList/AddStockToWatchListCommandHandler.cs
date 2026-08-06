using MediatR;
using Microsoft.EntityFrameworkCore;
using TrendSense.Application.Common.Exceptions;
using TrendSense.Application.Interfaces;
using TrendSense.Domain;

namespace TrendSense.Application.Features.WatchLists.Commands.AddStockToWatchList
{
    public class AddStockToWatchListCommandHandler : IRequestHandler<AddStockToWatchListCommand, Unit>
    {
        private readonly IAppDbContext _dbContext;
        private readonly ICurrentUserService _currentUser;

        public AddStockToWatchListCommandHandler(IAppDbContext dbContext, ICurrentUserService currentUser) =>
            (_dbContext, _currentUser) = (dbContext, currentUser);

        //public async Task<Unit> Handle(AddStockToWatchListCommand request, CancellationToken cancellationToken)
        //{
        //    var watchList = await _dbContext.WatchLists
        //        .Include(x => x.Items)
        //        .FirstOrDefaultAsync(x => x.Id == request.WatchListId && x.UserId == _currentUser.UserId, cancellationToken);

        //    if (watchList == null)
        //    {
        //        throw new NotFoundException(nameof(WatchList), request.WatchListId);
        //    }

        //    var exists = watchList.Items.Any(x => x.StockId == request.StockId);

        //    if (exists)
        //    {
        //        return Unit.Value;
        //    }

        //    watchList.Items.Add(new WatchListItem
        //    {
        //        Id = Guid.NewGuid(),
        //        StockId = request.StockId,
        //        WatchListId = request.WatchListId,
        //        AddedAt = DateTime.UtcNow
        //    });

        //    await _dbContext.SaveChangesAsync(cancellationToken);

        //    return Unit.Value;
        //}

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

            return Unit.Value;
        }
    }
}
