namespace TweetService.Application.UseCases.Queries.Writer.GetWriterById;

public record GetWriterByIdCommand
{
    public Guid Id { get; init; }
}