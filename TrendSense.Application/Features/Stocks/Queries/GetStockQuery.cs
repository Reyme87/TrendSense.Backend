using MediatR;
using TrendSense.Application.Dtos;

namespace TrendSense.Application.Features.Stocks.Queries
{
    public class GetStockQuery : IRequest<StockMarketInfo?>
    {
        public string SecId { get; set; } = null!;
    }
}
