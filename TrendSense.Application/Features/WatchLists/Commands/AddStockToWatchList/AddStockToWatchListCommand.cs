using MediatR;

namespace TrendSense.Application.Features.WatchLists.Commands.AddStockToWatchList
{
    public class AddStockToWatchListCommand : IRequest<Unit>
    {
        public Guid WatchListId { get; set; }
        public Guid StockId { get; set; }
    }
}
