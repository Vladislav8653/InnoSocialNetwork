using TweetService.Application.DTOs.WritersDto;

namespace TweetService.Application.Contracts.UseCasesContracts.WriterContracts;

public interface IWriterUpdateById
{
    Task<WriterResponseToDto> UpdateAsync(Guid id, WriterRequestToDto request, CancellationToken cancellationToken);
}