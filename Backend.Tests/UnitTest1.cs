namespace Backend.Tests;
using Moq;            // Fixes Mock<>
using Xunit;          // Fixes [Fact]
using System;
using System.Threading.Tasks;
public class UserServiceTests
{

    private readonly Mock<IUserRepository> _mockRepo;
    private readonly UserService _service;

    public UserServiceTests()
    {
        _mockRepo = new Mock<IUserRepository>();
        _service = new UserService(_mockRepo.Object);
    }
    [Fact]
    public async Task CreateUser_Success()
    {
        //Arrange-Act-Assert
        var user = new User { Name = "Test", Email = "a@a.com" };
        await _service.CreateUser(user);
        _mockRepo.Verify(r => r.AddAsync(user), Times.Once);
    }

    [Fact]
    public async Task GetUser_Success()
    {
        //Arrange
        var user = new User { UserId = 1, Name = "Test" };
        _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(user);

        //Act
        var result = await _service.ListUser(1);
        
        //Assert
        Assert.Equal(1, result.UserId);
    }

    [Fact]
    public async Task UpdateUser_Success()
    {
        //Arrange
        var user = new User { UserId = 1, Name = "Updated" };
        _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(user);

        //Act
        await _service.UpdateUser(user); 

        //Assert
        _mockRepo.Verify(r => r.UpdateAsync(It.IsAny<User>()), Times.Once);
    }

    [Fact]
    public async Task DeleteUser_Success()
    {
        var user = new User { UserId = 1 };
        _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(user);

        await _service.DeleteUser(1);

        _mockRepo.Verify(r => r.DeleteAsync(user), Times.Once);
    }
}
