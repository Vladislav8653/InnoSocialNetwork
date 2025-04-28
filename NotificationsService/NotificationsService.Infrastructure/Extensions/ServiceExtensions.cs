﻿using System.Text;
using FluentValidation;
using Hangfire;
using Hangfire.Mongo;
using Hangfire.Mongo.Migration.Strategies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using NotificationsService.Application.Contracts.RepositoryContracts;
using NotificationsService.Application.Contracts.ServicesContracts;
using NotificationsService.Application.EmailService;
using NotificationsService.Application.Settings;
using NotificationsService.Application.Validation;
using NotificationsService.Infrastructure.Repositories;

namespace NotificationsService.Infrastructure.Extensions;

public static class ServiceExtensions
{
    public static void AddMongoDb(this IServiceCollection services,
        IConfiguration configuration)
    {
        var mongoSettings = configuration.GetConnectionString("MongoDb");
        var client = new MongoClient(mongoSettings);
        var database = client.GetDatabase("NotificationsDb");

        services.AddSingleton(database);
    }
    
    public static void ConfigureRepositoryManager(this IServiceCollection services) =>
        services.AddScoped<INotificationRepository, NotificationRepository>();
    
    public static void AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<NotificationValidator>();
    }

    public static void ConfigureHangfire(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHangfire(config => config.UseMongoStorage(
            configuration.GetConnectionString("MongoHangfire"), 
            new MongoStorageOptions
            {
                MigrationOptions = new MongoMigrationOptions
                    { MigrationStrategy = new DropMongoMigrationStrategy() },
                CheckQueuedJobsStrategy = CheckQueuedJobsStrategy.TailNotificationsCollection
            }
        ));
        services.AddHangfireServer();
        //services.AddScoped<IHangfireService, HangfireService>();
    }
    
    

    public static void ConfigureEmailService(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<SmtpServiceSettings>(configuration.GetSection("SmtpServiceSettings"));
        services.AddSingleton<ISmtpService, SmtpService>();
    }
    
    
    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(s =>
        {
            s.SwaggerDoc("v1", new OpenApiInfo { Title = "Task manager API", Version = "v1"
            });
        });
    }
    
    public static void AddAuthorizationPolicy(this IServiceCollection services) =>
        services.AddAuthorization(options =>
        {
            options.AddPolicy("User", policy =>
                policy.RequireRole("User")); 
        });
    
    
    public static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("JwtSettings");
        var secretKey = jwtSettings["ValidIssuer"];
        
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["ValidIssuer"],
                    ValidAudience = jwtSettings["ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });
    }
}