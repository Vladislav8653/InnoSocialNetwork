using MediatR;
using NotificationsService.Application.Contracts;
using NotificationsService.Application.DTOs;
using NotificationsService.Application.UseCases.KafkaHandlers;
using NotificationsService.Infrastructure.BackgroundServices;
using NotificationsService.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.ConfigureKafka(builder.Configuration);
builder.Services.AddScoped<IEventHandler<SendEmailEvent>, SendEmailHandler>();
builder.Services.AddHostedService<KafkaListenerBackgroundService>();
builder.Services.ConfigureEmailService(builder.Configuration);
builder.Services.ConfigureGrpc(builder.Configuration);
builder.Services.ConfigureHangfire(builder.Configuration);

var app = builder.Build();
app.ConfigureExceptionHandler();
app.AddHangfireJobs();

app.Run();