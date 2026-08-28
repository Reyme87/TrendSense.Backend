using AutoMapper;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using TrendSense.Application.Common.Mappings;
using TrendSense.Application.Features.WatchLists.Queries.GetWatchLists;
using TrendSense.Application.Interfaces;
using TrendSense.Tests.Common;

namespace TrendSense.Tests.Models.WatchList
{
    public class GetWatchListsQueryTests : TestCrudBase
    {
        private readonly IMapper _mapper;
        private readonly Mock<ICurrentUserService> _currentUser;
        private readonly GetWatchListsQueryHandler _handler;

        public GetWatchListsQueryTests()
        {
            var applicationAssembly = typeof(GetWatchListsQueryHandler).Assembly;

            var myProfileInstance = new AssemblyMappingProfile(applicationAssembly);

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(myProfileInstance);
            }, NullLoggerFactory.Instance);

            _mapper = configurationProvider.CreateMapper();
            _currentUser = new Mock<ICurrentUserService>();

            _handler = new GetWatchListsQueryHandler(Context, _mapper, _currentUser.Object);
        }

        [Fact]
        public async Task GetWatchLists_ReturnsCurrentUserLists()
        {
            // Arrange
            _currentUser.Setup(x => x.UserId)
                .Returns(TrendSenseContextFactory.UserAId);

            // Act
            var result = await _handler.Handle(new GetWatchListsQuery(), CancellationToken.None);

            // Assert
            Assert.Single(result.WatchLists);

            var watchList = result.WatchLists.First();

            Assert.Equal(TrendSenseContextFactory.WatchListAId, watchList.Id);
        }
    }
}
