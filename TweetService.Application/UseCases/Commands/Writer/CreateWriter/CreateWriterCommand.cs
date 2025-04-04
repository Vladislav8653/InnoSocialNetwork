using MediatR;
using TweetService.Application.DTOs.WritersDto;

namespace TweetService.Application.UseCases.Commands.Writer.CreateWriter;

public record CreateWriterCommand : IRequest<Unit>
{
    public WriterRequestToDto WriterRequestToDto { get; init; }
}