using AutoFixture;
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

public class UsersTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly UsersController _controller;
    private readonly IFixture _fixture;

    public UsersTests()
    { 
        _fixture = new Fixture();
        _mediatorMock = new Mock<IMediator>();
        _controller = new UsersController(_mediatorMock.Object);
    }

    [Fact]
    public async Task GetAllUsers_ShouldReturnOkResult_WhenUsersAreFound()
    {
        // Arrange
        const int count = 3;
        var expectedUsers = _fixture.CreateMany<User>(count).ToList();
        
        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetAllUsersQuery>(), default))
            .ReturnsAsync(expectedUsers);   
        
        // Act
        var result = await _controller.GetAllUsers();
        
        //Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedUsers = Assert.IsAssignableFrom<IEnumerable<User>>(okResult.Value);
        Assert.Equal(count, returnedUsers.Count());
        
        _mediatorMock.Verify(m => m.Send(It.IsAny<GetAllUsersQuery>(), default), Times.Once);
    }

    [Fact]
    public async Task RegisterUser_ShouldReturnOkResult_WhenUserIsRegistered()
    {
        // Arrange
        var userRequestDto = _fixture.Create<UserRequestDto>();
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
        const string userId = "someUserId";
        var command = new DeleteUserCommand { UserId = userId };

        // Act
        var result = await _controller.DeleteUser(userId);

        // Assert
        _mediatorMock.Verify(m => m.Send(command, It.IsAny<CancellationToken>()), Times.Once);
        Assert.IsType<OkResult>(result);
    }
    
}