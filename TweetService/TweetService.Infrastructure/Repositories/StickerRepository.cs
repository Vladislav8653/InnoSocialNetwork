using TweetService.Application.Contracts.RepositoryContracts;
using TweetService.Domain.Models;

namespace TweetService.Infrastructure.Repositories;

public class StickerRepository(ApplicationContext context)
    : RepositoryBase<Sticker>(context), IStickerRepository
{
    
}