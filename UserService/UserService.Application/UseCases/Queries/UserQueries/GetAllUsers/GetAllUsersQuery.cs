using MediatR;
using UserService.Domain.Models;

namespace UserService.Application.UseCases.Queries.UserQueries.GetAllUsers;

public record GetAllUsersQuery : IRequest<IEnumerable<User>>;