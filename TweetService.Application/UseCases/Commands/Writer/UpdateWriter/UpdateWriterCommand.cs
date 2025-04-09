using MediatR;
using TweetService.Application.DTOs.WritersDto;

namespace TweetService.Application.UseCases.Commands.Writer.UpdateWriter;

public record UpdateWriterCommand : IRequest<Unit>
{
    public Guid Id { get; init; }
    public WriterRequestDto WriterRequestDto { get; init; }
}