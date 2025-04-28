using Hangfire;
using MediatR;
using NotificationsService.Infrastructure.BackgroundServices;
using NotificationsService.Infrastructure.Extensions;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMongoDb(builder.Configuration);
builder.Services.ConfigureRepositoryManager();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddValidators();
builder.Services.AddAuthorizationPolicy();
builder.Services.ConfigureJwt(builder.Configuration);
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddHostedService<EmailEventConsumerService>();
builder.Services.ConfigureEmailService(builder.Configuration);
builder.Services.ConfigureHangfire(builder.Configuration);

builder.Services.AddHttpContextAccessor();
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
builder.Services.ConfigureSwagger();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseHangfireDashboard();

app.UseSwagger();
app.UseSwaggerUI(s =>
{
    s.SwaggerEndpoint("/swagger/v1/swagger.json", "Task manager API");
});

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.ConfigureExceptionHandler();

app.MapControllers();

app.Run();