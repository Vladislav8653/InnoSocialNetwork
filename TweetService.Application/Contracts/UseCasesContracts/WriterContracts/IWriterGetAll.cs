using TweetService.Application.DTOs.WritersDto;

namespace TweetService.Application.Contracts.UseCasesContracts.WriterContracts;

public interface IWriterGetAll
{
    Task<IEnumerable<WriterResponseToDto>> GetAsync(CancellationToken cancellationToken);
}