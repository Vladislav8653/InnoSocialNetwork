using UserService.Application.DTO;
using UserService.Domain.Models;

namespace UserService.Application.Contracts.AuthenticationContracts;

public interface IAuthenticationManager
{
    Task<User> ValidateUser(AuthenticateUserDto userForAuth);
    
    Task<TokenDto> CreateTokens(User user, bool populateExp);
    
    public Task<string> CreateAccessToken(User user);
}