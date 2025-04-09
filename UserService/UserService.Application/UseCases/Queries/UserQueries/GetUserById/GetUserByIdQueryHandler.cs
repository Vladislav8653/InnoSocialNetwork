using MediatR;
using Microsoft.AspNetCore.Identity;
using UserService.Domain.CustomExceptions;
using UserService.Domain.Models;

namespace UserService.Application.UseCases.Queries.UserQueries.GetUserById;

public class GetUserByIdQueryHandler(
    UserManager<User> userManager) 
    : IRequestHandler<GetUserByIdQuery, User>
{
    public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.UserId);
        
        if (user == null)
        {
            throw new UserNotFoundException("User not found");
        }
        
        return user;
    }
}