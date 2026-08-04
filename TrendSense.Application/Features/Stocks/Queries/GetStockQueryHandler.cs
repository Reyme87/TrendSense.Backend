using AutoMapper;
using MediatR;
using TrendSense.Application.Dtos;
using TrendSense.Application.Interfaces;

namespace TrendSense.Application.Features.Stocks.Queries
{
    public class GetStockQueryHandler : IRequestHandler<GetStockQuery, StockMarketInfo?>
    {
        public readonly IStockMarketService _stockMarketService;

        public GetStockQueryHandler(IStockMarketService stockMarketService) =>
            _stockMarketService = stockMarketService;

        public async Task<StockMarketInfo?> Handle(GetStockQuery query, CancellationToken cancellationToken)
        {
            StockMarketInfo? stockInfo = await _stockMarketService.GetStockAsync(query.SecId, cancellationToken);

            return stockInfo;
        }
    }
}
