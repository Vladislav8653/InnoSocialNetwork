using Microsoft.EntityFrameworkCore;
using TweetService.Application;
using TweetService.Infrastructure;
using TweetService.Infrastructure.Extensions;
using TweetService.Presentation.GrpcService;

var builder = WebApplication.CreateBuilder(args);   
builder.Services.ConfigureRepository();
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.AddValidators();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(typeof(IApplicationMarker).Assembly));
builder.Services.AddGrpc();
builder.Services.AddControllers();
builder.Services.AddOpenApi();
var app = builder.Build();
app.ConfigureExceptionHandler();
app.UseRouting();
app.MapGrpcService<TweetDigestService>();
app.MapControllers();
app.MapOpenApi();

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
dbContext.Database.Migrate();

app.Run();