using MediatR;
using TrendSense.Application.Dtos;
using TrendSense.Domain;

namespace TrendSense.Application.Features.Stocks.Queries.GetDbStocks
{
    public class GetDbStocksQuery : IRequest<IReadOnlyList<StockDto>>
    {
    }
}
