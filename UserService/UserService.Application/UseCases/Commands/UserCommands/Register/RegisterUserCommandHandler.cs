using System.Data;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using UserService.Domain.Models;

namespace UserService.Application.UseCases.Commands.UserCommands.Register;

public class RegisterUserCommandHandler(IMapper mapper, UserManager<User> userManager)
    : IRequestHandler<RegisterUserCommand, IdentityResult>
{
    public async Task<IdentityResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var userDto = request.UserRequestDto;
        
        var user = mapper.Map<User>(userDto);
        
        var existingUser = await userManager.FindByNameAsync(userDto.UserName);
        if (existingUser != null)
        {
            throw new DuplicateNameException("User with such username already exists.");
        }
        
        var existingEmail = await userManager.FindByEmailAsync(userDto.Email);
        if (existingEmail != null)
        {
            throw new DuplicateNameException("User with such email already exists.");
        }
        
        var result = await userManager.CreateAsync(user, userDto.PasswordHash);
        if (result.Succeeded)
        {
            var userRoleAsString = userDto.Role.ToString();
            await userManager.AddToRolesAsync(user, new List<string> { userRoleAsString });
        }
        else
        {
            throw new BadHttpRequestException($"Cannot create a new user. {result}");
        }
        
        return result;
    }
}
