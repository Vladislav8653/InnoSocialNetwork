using TweetService.Application.Contracts.RepositoryContracts;
using TweetService.Domain.Models;

namespace TweetService.Infrastructure.Repositories;

public class WriterRepository(ApplicationContext context)
    : RepositoryBase<Writer>(context), IWriterRepository
{
    
}