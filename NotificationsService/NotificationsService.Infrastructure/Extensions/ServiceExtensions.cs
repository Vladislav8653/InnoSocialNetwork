using Confluent.Kafka;
using Hangfire;
using Hangfire.Mongo;
using Hangfire.Mongo.Migration.Strategies;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NotificationsService.Application.Contracts.Grpc;
using NotificationsService.Application.Contracts.ServicesContracts;
using NotificationsService.Application.EmailService;
using NotificationsService.Application.Settings;
using NotificationsService.Application.UseCases.HangfireHandlers;
using NotificationsService.Infrastructure.BackgroundServices;
using NotificationsService.Infrastructure.Grpc;
using NotificationsService.Infrastructure.Settings;
using TweetDigest.Grpc;

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
        
        services.AddSingleton<IAdminClient>(sp =>
        {
            var adminClientConfig = new AdminClientConfig { BootstrapServers = kafkaSettings.BootstrapServers };
            return new AdminClientBuilder(adminClientConfig)
                .SetErrorHandler((_, e) => sp.GetRequiredService<ILogger<IAdminClient>>().LogError($"Kafka AdminClient Error: {e.Reason}"))
                .SetLogHandler((_, log) => sp.GetRequiredService<ILogger<IAdminClient>>().LogInformation($"Kafka AdminClient Log: {log.Message}"))
                .Build();
        });
        
        services.AddHostedService<KafkaListenerBackgroundService>();

    }
    
  public static void ConfigureGrpc(this IServiceCollection services, IConfiguration configuration)
    { 
        services.AddScoped<ITweetDigestGrpcClient, TweetDigestGrpcClient>();
        services.AddGrpcClient<TweetService.TweetServiceClient>(o =>
        {
            o.Address = new Uri("http://tweet-service:5001"); 
        });
    }
  
    public static void ConfigureHangfire(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));
        var mongoSettings = services.BuildServiceProvider().GetService<IOptions<MongoDbSettings>>()?.Value;
        if (mongoSettings is null)
            throw new ApplicationException("mongoSettings not found");
        
        services.AddHangfire(config => config.UseMongoStorage(
            mongoSettings.ConnectionString,
            new MongoStorageOptions
            {
                MigrationOptions = new MongoMigrationOptions
                    { MigrationStrategy = new DropMongoMigrationStrategy() },
                Prefix = "hangfire",
                CheckQueuedJobsStrategy = CheckQueuedJobsStrategy.TailNotificationsCollection
            }
        ));
        services.AddHangfireServer();
        services.AddScoped<TweetDigestJob>();
    }
    
    public static void AddHangfireJobs(this IApplicationBuilder app)
    {
        var recurringJobs = app.ApplicationServices.GetRequiredService<IRecurringJobManager>();
        
        recurringJobs.AddOrUpdate<TweetDigestJob>(
            "daily-digest",
            job => job.ExecuteAsync(CancellationToken.None),
            Cron.Daily(10, 0) // каждый день в 10:00
        );
    }
    
}