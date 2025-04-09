using MediatR;
using UserService.Application.DTO;

namespace UserService.Application.UseCases.Commands.TokenCommands.RefreshToken;

public record RefreshTokenCommand : IRequest<string>
{
    public TokenDto TokenDto { get; init; } = null!;
}