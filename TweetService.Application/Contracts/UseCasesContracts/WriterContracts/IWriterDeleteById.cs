namespace TweetService.Application.Contracts.UseCasesContracts.WriterContracts;

public interface IWriterDeleteById
{
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}