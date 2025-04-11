using DiscussionService.Infrastructure.Extensions;
using DiscussionService.Infrastructure.Settings;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureRepository();
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));
var app = builder.Build();
app.Run();