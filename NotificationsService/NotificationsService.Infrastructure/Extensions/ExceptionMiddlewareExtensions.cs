using System.Net;
using System.Text.Json;
using AutoMapper;
using Confluent.Kafka;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using NotificationsService.Application.CustomExceptions;

namespace NotificationsService.Infrastructure.Extensions;

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
                    var exceptionDetails = new ExceptionDetails
                    {
                        Message = error.Message,
                        Type = error.GetType().Name, 
                    };
                    var response = context.Response;
                    response.ContentType = "application/json";
                    response.StatusCode = error switch
                    {
                        NotFoundException => (int)HttpStatusCode.NotFound,
                        InvalidOperationException => (int)HttpStatusCode.BadRequest,
                        ValidationException => (int)HttpStatusCode.BadRequest,
                        AutoMapperMappingException => (int)HttpStatusCode.BadRequest,
                        UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
                        JsonException => (int)HttpStatusCode.BadRequest,
                        ConsumeException => (int)HttpStatusCode.BadRequest,
                        _ => (int)HttpStatusCode.InternalServerError
                    };
                    var result = JsonSerializer.Serialize(exceptionDetails);
                    await response.WriteAsync(result);
                }
            });
        });
    }
}
