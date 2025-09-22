using MediatR;
using Microsoft.AspNetCore.Identity;
using UserService.Application.Contracts.AuthenticationContracts;
using UserService.Domain.Models;
using UserService.Application.Contracts;

namespace UserService.Application.UseCases.Commands.UserCommands.Authenticate;

public class AuthenticateUserCommandHandler(
    UserManager<User> userManager,
    IAuthenticationManager authManager)
    : IRequestHandler<AuthenticateUserCommand, (string AccessToken, string RefreshToken)>
{
    public async Task<(string AccessToken, string RefreshToken)> Handle(
        AuthenticateUserCommand request,
        CancellationToken cancellationToken)
    {
        var userForLogin = request.AuthenticateUserDto;
        
        var user = await authManager.ValidateUser(userForLogin);
        
        var tokenDto = await authManager.CreateTokens(user, populateExp: true);
        if (tokenDto.AccessToken == null || tokenDto.RefreshToken == null)
        {
            throw new UnauthorizedAccessException("Cannot create access or refresh token");
        }
        
        return (tokenDto.AccessToken, tokenDto.RefreshToken);
    }
}