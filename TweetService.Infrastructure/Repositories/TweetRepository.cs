using TweetService.Application.Contracts.RepositoryContracts;
using TweetService.Domain.Models;

namespace TweetService.Infrastructure.Repositories;

public class TweetRepository(ApplicationContext repositoryContext)
    : RepositoryBase<Tweet>(repositoryContext), ITweetRepository
{
    
}