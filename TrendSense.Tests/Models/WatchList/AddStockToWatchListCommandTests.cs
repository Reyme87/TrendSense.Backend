using Moq;
using TrendSense.Application.Common.Exceptions;
using TrendSense.Application.Features.WatchLists.Commands.AddStockToWatchList;
using TrendSense.Application.Interfaces;
using TrendSense.Tests.Common;

namespace TrendSense.Tests.Models.WatchList
{
    public class AddStockToWatchListCommandTests : TestCrudBase
    {
        private readonly Mock<ICurrentUserService> _currentUser;
        private readonly AddStockToWatchListCommandHandler _handler;

        public AddStockToWatchListCommandTests()
        {
            _currentUser = new Mock<ICurrentUserService>();

            _handler = new AddStockToWatchListCommandHandler(Context, _currentUser.Object);
        }

        [Fact]
        public async Task AddStock_Success()
        {
            // Arrange
            var command = new AddStockToWatchListCommand { WatchListId = TrendSenseContextFactory.WatchListAId, StockId = TrendSenseContextFactory.StockGazpromId };

            _currentUser.Setup(x => x.UserId)
                .Returns(TrendSenseContextFactory.UserAId);

            // Arrange
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            var item = Context.Items.Single(x => x.WatchListId == TrendSenseContextFactory.WatchListAId && x.StockId == TrendSenseContextFactory.StockGazpromId);

            Assert.Equal(item.WatchListId, TrendSenseContextFactory.WatchListAId);
            Assert.NotEqual(Guid.Empty, item.Id);
            Assert.Equal(item.StockId, TrendSenseContextFactory.StockGazpromId);
        }

        [Fact]
        public async Task AddStock_DoesNotAddDuplicate()
        {
            // Arrange
            var command = new AddStockToWatchListCommand
            {
                WatchListId = TrendSenseContextFactory.WatchListAId,
                StockId = TrendSenseContextFactory.StockSberId
            };

            _currentUser.Setup(x => x.UserId)
                .Returns(TrendSenseContextFactory.UserAId);

            var beforeCount = Context.Items.Count();

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            var afterCount = Context.Items.Count();
            Assert.Equal(beforeCount, afterCount);
        }

        [Fact]
        public async Task AddStock_CanNotAddToOtherUserWatchList()
        {
            // Arrange
            var command = new AddStockToWatchListCommand
            {
                WatchListId = TrendSenseContextFactory.WatchListAId,
                StockId = TrendSenseContextFactory.StockGazpromId
            };

            _currentUser.Setup(x => x.UserId)
                .Returns(TrendSenseContextFactory.UserBId);

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}
