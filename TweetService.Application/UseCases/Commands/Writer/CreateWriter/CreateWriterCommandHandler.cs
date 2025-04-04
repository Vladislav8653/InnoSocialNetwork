using MediatR;

namespace TweetService.Application.UseCases.Commands.Writer.CreateWriter;

public class CreateWriterCommandHandler : IRequestHandler<CreateWriterCommand, Unit>
{
    public async Task<Unit> Handle(CreateWriterCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}