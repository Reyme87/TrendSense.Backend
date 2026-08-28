using TrendSense.Application.Features.Stocks.Queries.GetDbStocks;
using TrendSense.Tests.Common;

namespace TrendSense.Tests.Models.Stocks
{
    public class GetDbStocksQueryTests : TestCrudBase
    {
        private readonly GetDbStocksQueryHandler _handler;

        public GetDbStocksQueryTests()
        {
            _handler = new GetDbStocksQueryHandler(Context);
        }

        [Fact]
        public async Task GetDbStocks_ReturnsStockFromDatabase()
        {
            // Arrange
            // Act
            var result = await _handler.Handle(new GetDbStocksQuery(), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Contains(result, x => x.TickerSymbol == "SBER");
            Assert.Contains(result, x => x.TickerSymbol == "GAZP");
        }
    }
}
