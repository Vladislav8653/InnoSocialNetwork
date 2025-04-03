using TweetService.Application.Contracts.UseCasesContracts.WriterContracts;
using TweetService.Application.DTOs.WritersDto;

namespace TweetService.Application.UseCases.WriterUseCases;

public class WriterGetAll : IWriterGetAll
{
    public async Task<IEnumerable<WriterResponseToDto>> GetAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}