using System.Reflection;
using TweetService.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);   
builder.Services.ConfigureRepository();
builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddControllers();

var app = builder.Build();
app.UseRouting();
app.MapControllers();
app.Run();