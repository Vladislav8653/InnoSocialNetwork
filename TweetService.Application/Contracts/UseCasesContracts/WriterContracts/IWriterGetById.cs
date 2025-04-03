using TweetService.Application.DTOs.WritersDto;

namespace TweetService.Application.Contracts.UseCasesContracts.WriterContracts;

public interface IWriterGetById
{
    Task<WriterResponseToDto> GetAsync(Guid id, CancellationToken cancellationToken);
}