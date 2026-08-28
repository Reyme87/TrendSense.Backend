using Microsoft.AspNetCore.Identity;
using Moq;
using TrendSense.Application.Features.Auth.Commands.Login;
using TrendSense.Application.Interfaces;
using TrendSense.Domain;

namespace TrendSense.Tests.Models.User
{
    public class LoginHandlerTest
    {
        private readonly Mock<UserManager<AppUser>> _userManager;
        private readonly Mock<IJwtTokenGenerator> _jwt;
        private readonly LoginUserCommandHandler _handler;

        public LoginHandlerTest()
        {
            var storeMock = new Mock<IUserStore<AppUser>>();
            _userManager = new Mock<UserManager<AppUser>>(
                storeMock.Object, null!, null!, null!, null!, null!, null!, null!, null!);

            _jwt = new Mock<IJwtTokenGenerator>();

            _handler = new LoginUserCommandHandler(_userManager.Object, _jwt.Object);
        }
        [Fact]
        public async Task ValidCredentials_ReturnsAuthResult()
        {
            // Arrange
            var command = new LoginUserCommand { Email = "test@mail.com", Password = "Password123!" };
            var user = new AppUser
            {
                Id = Guid.NewGuid(),
                Email = command.Email,
            };
            var expectedToken = new AuthResultDto { Token = "mocked-jwt-token" };

            _userManager.Setup(x => x.FindByEmailAsync(command.Email))
                .ReturnsAsync(user);
            _userManager.Setup(x => x.CheckPasswordAsync(user, command.Password))
                .ReturnsAsync(true);
            _jwt.Setup(x => x.GenerateToken(user))
                .Returns(expectedToken);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedToken.Token, result.Token);
        }
    } 
}
