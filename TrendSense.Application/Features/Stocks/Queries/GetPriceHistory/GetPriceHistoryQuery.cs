using MediatR;

namespace TrendSense.Application.Features.Stocks.Queries.GetPriceHistory
{
    public class GetPriceHistoryQuery : IRequest<IReadOnlyList<PriceHistoryDto>>
    {
        public Guid StockId { get; set; }
    }
}
