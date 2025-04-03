using TweetService.Application.Contracts.UseCasesContracts.WriterContracts;

namespace TweetService.Application.UseCases.WriterUseCases;

public class WriterDeleteById : IWriterDeleteById
{
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}