using Moq;
using TrendSense.Application.Dtos;
using TrendSense.Application.Features.Stocks.Queries.GetStock;
using TrendSense.Application.Interfaces;
using TrendSense.Tests.Common;

namespace TrendSense.Tests.Models.Stocks
{
    public class GetStocksQueryTests : TestCrudBase
    {
        private readonly Mock<IStockMarketService> _marketService;
        private readonly GetStockQueryHandler _handler;
        
        public GetStocksQueryTests()
        {
            _marketService = new Mock<IStockMarketService>();
            _handler = new GetStockQueryHandler(_marketService.Object);
        }

        [Fact]
        public async Task GetStocks_ReturnsStockFromMarketService()
        {
            // Arrange
            _marketService.Setup(x => x.GetStockAsync("SBER", CancellationToken.None))
                .ReturnsAsync(new StockMarketInfo
                {
                    SecId = "SBER",
                    BoardId = "TQBR",
                    ShortName = "Сбербанк",
                    Isin = "RU0009029540",
                    CurrencyId = "SUR",
                    Last = 300
                });

            var query = new GetStockQuery { SecId = "SBER" };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("SBER", result.SecId);
            Assert.Equal("Сбербанк", result.ShortName);
        }
    }
}
