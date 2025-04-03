using TweetService.Application.Contracts.UseCasesContracts.WriterContracts;
using TweetService.Application.DTOs.WritersDto;

namespace TweetService.Application.UseCases.WriterUseCases;

public class WriterCreate : IWriterCreate
{
    public async Task<WriterResponseToDto> CreateAsync(WriterRequestToDto request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}