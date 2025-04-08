using Microsoft.Extensions.DependencyInjection;
using TweetService.Application.Contracts.RepositoryContracts;
using TweetService.Infrastructure.Repositories;

namespace TweetService.Infrastructure.Extensions;

public static class ServiceExtension
{
    public static void ConfigureRepository(this IServiceCollection services)
    {
        services.AddScoped<ITweetRepository, TweetRepository>();
    }
    
}