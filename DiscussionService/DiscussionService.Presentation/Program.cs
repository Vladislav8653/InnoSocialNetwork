using DiscussionService.Application;
using DiscussionService.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureMongoDb(builder.Configuration);
builder.Services.ConfigureCacheExpireTime(builder.Configuration);
builder.Services.AddRedisCache(builder.Configuration);
builder.Services.AddMemoryCache();
builder.Services.ConfigureRepository();
builder.Services.AddValidators();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(typeof(IApplicationMarker).Assembly));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
app.ConfigureExceptionHandler();
app.UseRouting();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();
app.Run();