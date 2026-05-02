using MediatR;

namespace TrendSense.Application.Features.WatchLists.Commands.DeleteWatchList
{
    public class DeleteWatchListCommand : IRequest<Unit>
    {
        public Guid WatchListId { get; set; }
    }
}
