using DiscussionService.Application.Contracts;
using DiscussionService.Application.Validation;
using DiscussionService.Infrastructure.Repositories;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace DiscussionService.Infrastructure.Extensions;

public static class ServiceExtension
{
    public static void ConfigureRepository(this IServiceCollection services)
    {
        services.AddScoped<IMessageRepository, MessageRepository>();
    }
    
    public static void AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<MessageValidator>();
    }
    
}