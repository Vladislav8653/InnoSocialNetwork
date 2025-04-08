using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TweetService.Application.Contracts.RepositoryContracts;
using TweetService.Application.Validation;
using TweetService.Infrastructure.Repositories;

namespace TweetService.Infrastructure.Extensions;

public static class ServiceExtension
{
    public static void ConfigureRepository(this IServiceCollection services)
    {
        services.AddScoped<ITweetRepository, TweetRepository>();
    }
    
    public static void AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<TweetValidator>();
    }
}