using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrendSense.Application.Dtos;
using TrendSense.Application.Features.Stocks.Commands;
using TrendSense.Application.Features.Stocks.Queries.GetDbStocks;
using TrendSense.Application.Features.Stocks.Queries.GetStock;

namespace TrendSense.WebApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class StocksController : BaseController
    {
        public StocksController(IMediator mediator) : base(mediator) { }

        ///// <summary>
        ///// Gets the stock by its SecId
        ///// </summary>
        ///// <param name="command"></param>
        ///// <returns>
        ///// Returns StockMarketInfo
        ///// </returns>
        ///// <response code="200">Success</response>
        ///// <response code="401">If the user is unauthorized</response>
        //[HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //public async Task<ActionResult<StockMarketInfo>> Get([FromQuery] GetStockQuery query, CancellationToken cancellationToken)
        //{
        //    var result = await Mediator.Send(query, cancellationToken);

        //    if (result is null)
        //        return NotFound();

        //    return Ok(result);
        //}

        /// <summary>
        /// Gets stocks list
        /// </summary>
        /// <param name="command"></param>
        /// <returns>
        /// Returns StockDto
        /// </returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<StockDto>> Get([FromQuery] GetDbStocksQuery query, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(query, cancellationToken);

            return Ok(result);
        }

        [HttpPost("sync")]
        public async Task<IActionResult> Sync(SyncStocksCommand command, CancellationToken cancellationToken)
        {
            await Mediator.Send(command, cancellationToken);

            return NoContent();
        }
    }
}
