using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TweetService.Application.Contracts.ProducerContracts;
using TweetService.Application.Contracts.RepositoryContracts;
using TweetService.Application.Validation;
using TweetService.Infrastructure.Producers;
using TweetService.Infrastructure.Repositories;
using TweetService.Infrastructure.Settings;

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
    
    public static void ConfigureKafka(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<KafkaSettings>(configuration.GetSection("KafkaSettings"));
        
        services.AddSingleton<ITweetDeletedProducer, TweetDeletedProducer>();

    }
}