using MediatR;

namespace TweetService.Application.UseCases.Commands.Writer.DeleteWriter;

public record DeleteWriterCommand : IRequest<Unit>
{
    public Guid Id {get; init;}
}