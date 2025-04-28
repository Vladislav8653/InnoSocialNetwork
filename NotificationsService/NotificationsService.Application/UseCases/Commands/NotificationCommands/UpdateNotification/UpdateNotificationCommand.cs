using MediatR;
using NotificationsService.Application.DTOs.NotificationsDto;

namespace NotificationsService.Application.UseCases.Commands.NotificationCommands.UpdateNotification;

public record UpdateNotificationCommand : IRequest<Unit>
{
    public NotificationRequestDto NotificationDto { get; init; }
}