using TweetService.Application;
using TweetService.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);   
builder.Services.ConfigureRepository();
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.AddValidators();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(typeof(IApplicationMarker).Assembly));
builder.Services.AddControllers();
builder.Services.AddOpenApi();
var app = builder.Build();
app.ConfigureExceptionHandler();
app.UseRouting();
app.MapControllers();
app.MapOpenApi();
app.Run();