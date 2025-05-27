using Hangfire;
using MediatR;
using NotificationsService.Application.Contracts;
using NotificationsService.Application.DTOs;
using NotificationsService.Application.GRPC;
using NotificationsService.Application.UseCases.HangfireHandlers;
using NotificationsService.Application.UseCases.KafkaHandlers;
using NotificationsService.Infrastructure.BackgroundServices;
using NotificationsService.Infrastructure.Extensions;
using Tweet.Grpc;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.ConfigureKafka(builder.Configuration);
builder.Services.AddScoped<IEventHandler<SendEmailEvent>, SendEmailHandler>();
builder.Services.AddHostedService<KafkaListenerBackgroundService>();
builder.Services.ConfigureEmailService(builder.Configuration);
builder.Services.ConfigureGrpc(builder.Configuration);
builder.Services.ConfigureHangfire(builder.Configuration);
builder.Services.AddHangfireJobs();


var app = builder.Build();

app.ConfigureExceptionHandler();





app.Run();