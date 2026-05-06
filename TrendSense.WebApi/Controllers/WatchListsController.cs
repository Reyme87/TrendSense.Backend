using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrendSense.Application.Features.WatchLists.Commands.AddStockToWatchList;
using TrendSense.Application.Features.WatchLists.Commands.CreateWatchList;
using TrendSense.Application.Features.WatchLists.Commands.DeleteWatchList;
using TrendSense.Application.Features.WatchLists.Commands.RemoveStockFromWatchList;
using TrendSense.Application.Features.WatchLists.Queries.GetWatchLists;

namespace TrendSense.WebApi.Controllers
{
    [Authorize]
    [Route("api/watchlists")]
    public class WatchListsController : BaseController
    {
        public WatchListsController(IMediator mediator) : base(mediator) { }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create(CreateWatchListCommand command)
        {
            var id = await Mediator.Send(command);
            return Ok(id);
        }

        [HttpGet]
        public async Task<ActionResult<WatchListVm>> Get()
        {
            var result = await Mediator.Send(new GetWatchListsQuery());
            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var command = new DeleteWatchListCommand
            {
                WatchListId = id
            };
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPost("{id:guid}/stocks")]
        public async Task<ActionResult> AddStock(Guid id, AddStockToWatchListCommand command)
        {
            if (id != command.WatchListId)
            {
                return BadRequest();
            }

            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id:guid}/stocks/{stockId:guid}")]
        public async Task<ActionResult> RemoveStock(Guid id, Guid stockId)
        {
            var command = new RemoveStockFromWatchListCommand
            {
                WatchListId = id,
                StockId = stockId
            };

            await Mediator.Send(command);

            return NoContent(); 
        }
    }
}
