using DiscussionService.Application;
using DiscussionService.Infrastructure.Extensions;
using DiscussionService.Infrastructure.Settings;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureRepository();
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));
builder.Services.AddValidators();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(typeof(IApplicationMarker).Assembly));
builder.Services.AddControllers();
var app = builder.Build();
app.ConfigureExceptionHandler();
app.UseRouting();
app.MapControllers();
app.Run();