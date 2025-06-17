using MediatR;
using UserService.Domain.Models;

namespace UserService.Application.UseCases.Queries.UserQueries.GetUserById;

public record GetUserByIdQuery : IRequest<User>
{
    public string UserId { get; init; } = null!;
}