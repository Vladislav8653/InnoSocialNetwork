using MediatR;

namespace TweetService.Application.UseCases.Commands.Writer.DeleteWriter;

public class DeleteWriterCommandHandler : IRequestHandler<DeleteWriterCommand, Unit>
{
    public async Task<Unit> Handle(DeleteWriterCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}