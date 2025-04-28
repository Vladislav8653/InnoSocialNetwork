using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotificationsService.Application.DTOs.NotificationsDto;
using NotificationsService.Application.UseCases.Commands.NotificationCommands.CreateNotification;
using NotificationsService.Application.UseCases.Commands.NotificationCommands.DeleteNotification;
using NotificationsService.Application.UseCases.Commands.NotificationCommands.UpdateNotification;
using NotificationsService.Application.UseCases.Queries.NotificationQueries.GetAllNotifications;
using NotificationsService.Application.UseCases.Queries.NotificationQueries.GetNotificationById;

namespace NotificationsService.Presentation.Controllers;

[Route("api/notifications")]
[ApiController]
public class NotificationController(
    IMediator mediator) : Controller
{
    [HttpGet]
    [Authorize(Policy = "User")]
    public async Task<IActionResult> GetNotifications(CancellationToken cancellationToken)
    {
        var query = new GetAllNotificationsQuery();
        var notifications = await mediator.Send(query, cancellationToken);
        
        return Ok(notifications);
    }

    [HttpGet("{id:guid}")]
    [Authorize(Policy = "User")]
    public async Task<IActionResult> GetNotificationById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetNotificationQuery
        {
            NotificationId = id
        };
        var notification = await mediator.Send(query, cancellationToken);
        
        return Ok(notification);
    }
    
    [HttpPost]
    [Authorize(Policy = "User")]
    public async Task<IActionResult> AddNotification(
        [FromBody] NotificationRequestDto notificationDto,
        CancellationToken cancellationToken)
    {
        var command = new CreateNotificationCommand
        {
            NotificationDto = notificationDto
        };
        
        await mediator.Send(command, cancellationToken);
        
        return NoContent();
    }

    [HttpPut]
    [Authorize(Policy = "User")]
    public async Task<IActionResult> UpdateNotification(
        [FromBody] NotificationRequestDto notificationDto,
        CancellationToken cancellationToken)
    {
        var command = new UpdateNotificationCommand
        {
            NotificationDto = notificationDto
        };
        await mediator.Send(command, cancellationToken);
        
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Policy = "User")]
    public async Task<IActionResult> DeleteNotification(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteNotificationCommand
        {
            Id = id
        };
        await mediator.Send(command, cancellationToken);
        
        return NoContent();
    }
}