using MediatR;

namespace TrendSense.Application.Features.WatchLists.Commands.RemoveStockFromWatchList
{
    public class RemoveStockFromWatchListCommand : IRequest<Unit>
    {
        public Guid WatchListId { get; set; }
        public Guid StockId { get; set; }
    }
}
