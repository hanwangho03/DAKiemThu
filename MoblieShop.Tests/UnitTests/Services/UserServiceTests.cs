using MoblieShop.Repository;
using MoblieShop.Service;
using MoblieShop.Tests.MockData;
using Moq;
using WebDoDienTu.Models;
using WebDoDienTu.ViewModels;

namespace MoblieShop.Tests.UnitTests.Services
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IRoleRepository> _roleRepositoryMock;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _roleRepositoryMock = new Mock<IRoleRepository>();
            _userService = new UserService(_userRepositoryMock.Object, _roleRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllUsersAsync_ShouldReturnUsers()
        {
            // Arrange
            var users = UserMockData.GetUsers();
            _userRepositoryMock.Setup(r => r.GetAllUsersAsync()).ReturnsAsync(users);

            // Act
            var result = await _userService.GetAllUsersAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(users.Count, result.Count());
        }

        [Fact]
        public async Task GetUserByIdAsync_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var user = UserMockData.GetUserById("1");
            _userRepositoryMock.Setup(r => r.GetUserByIdAsync("1")).ReturnsAsync(user);

            // Act
            var result = await _userService.GetUserByIdAsync("1");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Email, result.Email);
        }

        [Fact]
        public async Task GetUserByIdAsync_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arrange
            _userRepositoryMock.Setup(r => r.GetUserByIdAsync("99")).ReturnsAsync((ApplicationUser)null);

            // Act
            var result = await _userService.GetUserByIdAsync("99");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateUserAsync_ShouldReturnTrue_WhenUserCreated()
        {
            // Arrange
            var model = new CreateUserViewModel
            {
                Email = "newuser@test.com",
                Password = "Password123",
                FirstName = "New",
                LastName = "User"
            };

            _userRepositoryMock.Setup(r => r.CreateUserAsync(It.IsAny<ApplicationUser>(), model.Password)).ReturnsAsync(true);

            // Act
            var result = await _userService.CreateUserAsync(model);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteUserAsync_ShouldReturnTrue_WhenUserDeleted()
        {
            // Arrange
            _userRepositoryMock.Setup(r => r.HasOrdersAsync("1")).ReturnsAsync(false);
            _userRepositoryMock.Setup(r => r.DeleteUserAsync("1")).ReturnsAsync(true);

            // Act
            var result = await _userService.DeleteUserAsync("1");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteUserAsync_ShouldReturnFalse_WhenUserHasOrders()
        {
            // Arrange
            _userRepositoryMock.Setup(r => r.HasOrdersAsync("1")).ReturnsAsync(true);

            // Act
            var result = await _userService.DeleteUserAsync("1");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task UpdateUserAsync_ShouldReturnTrue_WhenUserUpdated()
        {
            // Arrange
            var user = UserMockData.GetUserById("1");
            var model = new EditUserViewModel
            {
                Id = user.Id,
                Email = "updated@test.com",
                UserName = "updateduser",
                FirstName = "Updated",
                LastName = "User"
            };

            _userRepositoryMock.Setup(r => r.GetUserByIdAsync(user.Id)).ReturnsAsync(user);
            _userRepositoryMock.Setup(r => r.UpdateUserAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(true);

            // Act
            var result = await _userService.UpdateUserAsync(model);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task ChangeUserRoleAsync_ShouldReturnTrue_WhenRoleUpdated()
        {
            // Arrange
            var user = UserMockData.GetUserById("1");
            _userRepositoryMock.Setup(r => r.GetUserByIdAsync("1")).ReturnsAsync(user);
            _userRepositoryMock.Setup(r => r.UpdateUserRolesAsync(user, "Admin")).ReturnsAsync(true);

            // Act
            var result = await _userService.ChangeUserRoleAsync("1", "Admin");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task BlockUserAsync_ShouldReturnTrue_WhenUserBlocked()
        {
            // Arrange
            _userRepositoryMock.Setup(r => r.BlockUserAsync("1")).ReturnsAsync(true);

            // Act
            var result = await _userService.BlockUserAsync("1");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task UnblockUserAsync_ShouldReturnTrue_WhenUserUnblocked()
        {
            // Arrange
            _userRepositoryMock.Setup(r => r.UnblockUserAsync("1")).ReturnsAsync(true);

            // Act
            var result = await _userService.UnblockUserAsync("1");

            // Assert
            Assert.True(result);
        }
    }
}
