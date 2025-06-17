using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserService.Domain.Models;

namespace UserService.Application.UseCases.Queries.UserQueries.GetAllUsers;

public class GetAllUsersQueryHandler(
    UserManager<User> userManager) 
    : IRequestHandler<GetAllUsersQuery, IEnumerable<User>>
{
    public async Task<IEnumerable<User>> Handle(
        GetAllUsersQuery request, 
        CancellationToken cancellationToken)
    {
        var users = await userManager.Users.ToListAsync(cancellationToken);
            
        return users;
    }
}