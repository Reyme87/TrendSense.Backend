using MediatR;
using TrendSense.Domain;

namespace TrendSense.Application.Features.WatchLists.Commands
{
    public class CreateWatchListCommand : IRequest<Guid>
    {
        public string Name { get; set; }
    }
}
