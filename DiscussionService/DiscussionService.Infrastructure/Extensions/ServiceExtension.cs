using DiscussionService.Application;
using DiscussionService.Application.Contracts;
using DiscussionService.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DiscussionService.Infrastructure.Extensions;

public static class ServiceExtension
{
    public static void ConfigureRepository(this IServiceCollection services)
    {
        services.AddScoped<IMessageRepository, MessageRepository>();
    }
    
}