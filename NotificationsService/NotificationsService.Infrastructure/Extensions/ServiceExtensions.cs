using Confluent.Kafka;
using FluentValidation;
using Hangfire;
using Hangfire.Mongo;
using Hangfire.Mongo.Migration.Strategies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NotificationsService.Application.Contracts.ServicesContracts;
using NotificationsService.Application.EmailService;
using NotificationsService.Application.GRPC;
using NotificationsService.Application.Settings;
using NotificationsService.Application.UseCases.HangfireHandlers;
using NotificationsService.Application.Validation;
using NotificationsService.Infrastructure.Settings;
using Tweet.Grpc;

namespace NotificationsService.Infrastructure.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureEmailService(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<SmtpServiceSettings>(configuration.GetSection("SmtpServiceSettings"));
        services.AddSingleton<ISmtpService, SmtpService>();
    }
    
    public static void ConfigureKafka(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<KafkaSettings>(configuration.GetSection("KafkaSettings"));
        var kafkaSettings = services.BuildServiceProvider().GetService<IOptions<KafkaSettings>>()?.Value;
        if (kafkaSettings is null)
            throw new ApplicationException("KafkaSettings not found");
        services.AddSingleton<IConsumer<string, string>>(sp =>
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = kafkaSettings.BootstrapServers,
                GroupId = kafkaSettings.GroupId,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            return new ConsumerBuilder<string, string>(config).Build();
        });
    }

    public static void ConfigureHangfire(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHangfire(config => config.UseMongoStorage(
            configuration.GetConnectionString("MongoHangfire"), 
            new MongoStorageOptions
            {
                MigrationOptions = new MongoMigrationOptions
                    { MigrationStrategy = new DropMongoMigrationStrategy() },
                Prefix = "hangfire",
                CheckQueuedJobsStrategy = CheckQueuedJobsStrategy.TailNotificationsCollection
            }
        ));
        services.AddScoped<TweetDigestJob>();
    }
    
    public static void ConfigureGrpc(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddGrpcClient<TweetService.TweetServiceClient>(o =>
        {
            o.Address = new Uri("http://tweet-service:5001"); 
        });
        services.AddScoped<TweetDigestGrpcClient>();
    }

    public static void AddHangfireJobs(this IServiceCollection services)
    {
        RecurringJob.AddOrUpdate<TweetDigestJob>(
            "daily-digest",
            job => job.ExecuteAsync(CancellationToken.None),
            Cron.Daily(10, 0)
        );
    }
    
}