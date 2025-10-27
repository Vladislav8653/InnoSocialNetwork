using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using MediatR;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using UserService.Application.Contracts.AuthenticationContracts;
using UserService.Application.MappingProfiles;
using UserService.Infrastructure;
using UserService.Infrastructure.Extensions;
using UserService.Infrastructure.Services;
using UserService.Presentation;

var builder = WebApplication.CreateBuilder(args);

string policyName = builder.Configuration["PolicyName"] ?? "origins"; 
builder.Services.ConfigureCors(builder.Configuration, policyName);
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.AddAutoMapper(typeof(UserMappingProfile).Assembly);
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthenticationManager, AuthenticationManager>();
builder.Services.ConfigureKafka(builder.Configuration);
builder.Services.ConfigureNotificationService();
builder.Services.AddHttpClient();
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.ConfigureIdentity();
builder.Services.AddAuthorizationPolicy();
builder.Services.ConfigureJwt(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddApiVersioning(options =>
    {
        options.ReportApiVersions =
            true; // Сообщать о поддерживаемых версиях в заголовке ответа (api-supported-versions)
        options.AssumeDefaultVersionWhenUnspecified =
            true; // Использовать версию по умолчанию, если клиент ее не указал
        options.DefaultApiVersion = new ApiVersion(1, 0);
        options.ApiVersionReader = new UrlSegmentApiVersionReader(); // Как читать версию из запроса - чтение из URL
    }).AddMvc()
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.ConfigureSwagger();

var app = builder.Build();

app.ConfigureExceptionHandler();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    foreach (var description in provider.ApiVersionDescriptions.Reverse())
    {
        options.SwaggerEndpoint(
            $"/swagger/{description.GroupName}/swagger.json",
            description.GroupName.ToUpperInvariant());
    }
    options.RoutePrefix = string.Empty;
});
app.UseCors(policyName);
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.ApplyMigrations();
app.Run();
