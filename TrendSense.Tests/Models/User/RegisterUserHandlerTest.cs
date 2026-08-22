using Microsoft.AspNetCore.Identity;
using Moq;
using System.ComponentModel.DataAnnotations;
using TrendSense.Application.Features.Auth.Commands.Register;
using TrendSense.Domain;

namespace TrendSense.Tests.Models.User
{
    public class RegisterUserHandlerTest
    {
        private readonly Mock<UserManager<AppUser>> _userManager;
        private readonly RegisterUserCommandHandler _handler;

        public RegisterUserHandlerTest()
        {
            var storeMock = new Mock<IUserStore<AppUser>>();
            _userManager = new Mock<UserManager<AppUser>>(storeMock.Object, null!, null!, null!, null!, null!, null!, null!, null!);
            _handler = new RegisterUserCommandHandler(_userManager.Object);
        }

        [Fact]
        public async Task RegisterUser_ReturnUserId()
        {
            // Arrange
            var command = new RegisterUserCommand { Email = "test@mail.com", UserName = "testUser", Password = "Password123!" };

            _userManager.Setup(x => x.FindByEmailAsync(command.Email))
                .ReturnsAsync((AppUser)null!);

            _userManager.Setup(x => x.CreateAsync(It.IsAny<AppUser>(), command.Password))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotEqual(Guid.Empty, result);
        }

        [Fact]
        public async Task RegisterUser_AlreadyExists()
        {
            // Arrange
            var command = new RegisterUserCommand { Email = "existing@mail.com" };

            _userManager.Setup(x => x.FindByEmailAsync(command.Email))
                .ReturnsAsync(new AppUser { Email = command.Email });

            // Act
            // Assert
            await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}
