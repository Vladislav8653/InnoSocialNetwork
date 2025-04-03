using TweetService.Application.DTOs.WritersDto;

namespace TweetService.Application.Contracts.UseCasesContracts.WriterContracts;

public interface IWriterCreate
{
    Task<WriterResponseToDto> CreateAsync(WriterRequestToDto request, CancellationToken cancellationToken);
}