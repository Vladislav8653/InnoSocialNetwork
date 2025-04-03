using TweetService.Application.Contracts.UseCasesContracts.WriterContracts;
using TweetService.Application.DTOs.WritersDto;

namespace TweetService.Application.UseCases.WriterUseCases;

public class WriterUpdateById : IWriterUpdateById
{
    public async Task<WriterResponseToDto> UpdateAsync(Guid id, WriterRequestToDto request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}