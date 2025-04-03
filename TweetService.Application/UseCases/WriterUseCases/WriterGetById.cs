using TweetService.Application.Contracts.UseCasesContracts.WriterContracts;
using TweetService.Application.DTOs.WritersDto;

namespace TweetService.Application.UseCases.WriterUseCases;

public class WriterGetById : IWriterGetById
{
    public async Task<WriterResponseToDto> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}