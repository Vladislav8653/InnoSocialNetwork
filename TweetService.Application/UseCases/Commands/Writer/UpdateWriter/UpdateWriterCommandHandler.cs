using MediatR;

namespace TweetService.Application.UseCases.Commands.Writer.UpdateWriter;

public class UpdateWriterCommandHandler : IRequestHandler<UpdateWriterCommand, Unit>
{
    public async Task<Unit> Handle(UpdateWriterCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}