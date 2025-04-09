using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using UserService.Application.DTO;
using UserService.Application.UseCases.Commands.UserCommands.DeleteById;
using UserService.Application.UseCases.Commands.UserCommands.Register;
using UserService.Application.UseCases.Queries.UserQueries.GetAllUsers;
using UserService.Domain.Models;
using UserService.Presentation.Controllers;

namespace UserService.Tests;

public class UsersControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly UsersController _controller;

    public UsersControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new UsersController(_mediatorMock.Object);
    }

    [Fact]
    public async Task GetAllUsers_ShouldReturnOkResult_WhenUsersAreFound()
    {
        // Arrange
        var users = new List<AuthenticateUserDto>(); // Предположим, что UserDto - это DTO для пользователей
        var userResponse = new List<User>();
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllUsersQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(userResponse);

        // Act
        var result = await _controller.GetAllUsers();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(users, okResult.Value);
    }

    [Fact]
    public async Task RegisterUser_ShouldReturnOkResult_WhenUserIsRegistered()
    {
        // Arrange
        var userRequestDto = new UserRequestDto(); // Заполните нужными данными
        var command = new RegisterUserCommand { UserRequestDto = userRequestDto };
        
        // Act
        var result = await _controller.RegisterUser(userRequestDto);

        // Assert
        _mediatorMock.Verify(m => m.Send(command, It.IsAny<CancellationToken>()), Times.Once);
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task DeleteUser_ShouldReturnOkResult_WhenUserIsDeleted()
    {
        // Arrange
        var userId = "someUserId";
        var command = new DeleteUserCommand { UserId = userId };

        // Act
        var result = await _controller.DeleteUser(userId);

        // Assert
        _mediatorMock.Verify(m => m.Send(command, It.IsAny<CancellationToken>()), Times.Once);
        Assert.IsType<OkResult>(result);
    }
    
}