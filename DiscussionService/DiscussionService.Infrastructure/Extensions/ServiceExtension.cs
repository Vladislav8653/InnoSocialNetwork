using DiscussionService.Application.Contracts;
using DiscussionService.Application.Validation;
using DiscussionService.Infrastructure.Repositories;
using DiscussionService.Infrastructure.Settings;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace DiscussionService.Infrastructure.Extensions;

public static class ServiceExtension
{
    public static void ConfigureRepository(this IServiceCollection services)
    {
        services.AddScoped<MessageRepository>();
        services.AddScoped<IMessageRepository, CachedMessageRepository>();
    }

    public static void AddRedisCache(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetSection("RedisServerAddress").Value;
        });
    }



    public static void AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<MessageValidator>();
    }
    
    public static void ConfigureMongoDb(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));
        services.AddSingleton<IMongoClient>(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
            return new MongoClient(settings.ConnectionString);
        });
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
    }

    public static void ConfigureCacheExpireTime(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<CacheExpireTimeSettings>(configuration.GetSection("CacheExpireTimeSettings"));
    }
    
}