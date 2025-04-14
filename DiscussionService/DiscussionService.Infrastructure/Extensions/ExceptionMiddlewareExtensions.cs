using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace DiscussionService.Infrastructure.Extensions;

public static class ExceptionMiddlewareExtensions
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                context.Response.ContentType = "application/json";
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    var error = contextFeature.Error;
                    var exceptionDetails = new ExceptionDetails()
                    {
                        Message = error.Message, 
                        Type = error.GetType().Name,
                    };
                    var response = context.Response;
                    response.ContentType = "application/json";
                    response.StatusCode = error switch
                    {
                        InvalidOperationException => (int)HttpStatusCode.BadRequest,
                        _ => (int)HttpStatusCode.InternalServerError
                    };
                    var result = JsonSerializer.Serialize(exceptionDetails);
                    await response.WriteAsync(result);
                }
            });
        });
    }
}
