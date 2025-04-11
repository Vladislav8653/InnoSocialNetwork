using DiscussionService.Application;
using Microsoft.Extensions.DependencyInjection;

namespace DiscussionService.Infrastructure.Extensions;

public static class ServiceExtension
{
    public static void ConfigureRepository(this IServiceCollection services)
    {
        services.AddScoped<IMessageRepository, MessageRepository>();
    }
    
}