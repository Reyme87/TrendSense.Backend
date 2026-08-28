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
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class WatchListsController : BaseController
    {
        public WatchListsController(IMediator mediator) : base(mediator) { }

        /// <summary>
        /// Creates a new watchlist
        /// </summary>
        /// <remarks>
        /// Creates a new stocks watchlist
        /// </remarks>
        /// <param name="command"></param>
        /// <returns>
        /// Returns Guid of the created watchlist
        /// </returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateWatchListCommand command)
        {
            var id = await Mediator.Send(command);
            return Ok(id);
        }
        /// <summary>
        /// Gets the wathchlists collection
        /// </summary>
        /// <returns>
        /// Returns WatchListVm
        /// </returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<WatchListVm>> Get()
        {
            var result = await Mediator.Send(new GetWatchListsQuery());
            return Ok(result);
        }

        /// <summary>
        /// Deletes the selected watchlist
        /// </summary>
        /// <param name="id">Watchlist Id (Guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpDelete("{id:guid}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> Delete(Guid id)
        {
            var command = new DeleteWatchListCommand
            {
                WatchListId = id
            };
            await Mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Adds stock to a watchlist
        /// </summary>
        /// <param name="id">WatchList Id (Guid)</param>
        /// <param name="command"></param>
        /// <returns>Returns NoContent</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost("{id:guid}/stocks")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> AddStock(Guid id, [FromBody] AddStockToWatchListCommand command)
        {
            if (id != command.WatchListId)
            {
                return BadRequest();
            }

            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Removes stock from the watchlist
        /// </summary>
        /// <param name="id">WatchList Id (Guid)</param>
        /// <param name="stockId">Stock Id (Guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpDelete("{id:guid}/stocks/{stockId:guid}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
