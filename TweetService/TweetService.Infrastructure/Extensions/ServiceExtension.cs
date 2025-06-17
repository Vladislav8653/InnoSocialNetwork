using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        services.AddScoped<IStickerRepository, StickerRepository>();
    }
    
    public static void AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<TweetValidator>();
        services.AddValidatorsFromAssemblyContaining<StickerValidator>();
    }
    
    public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
        services.AddDbContext<ApplicationContext>(opts =>
            opts.UseNpgsql(configuration["PostgresConnectionString"]));
}