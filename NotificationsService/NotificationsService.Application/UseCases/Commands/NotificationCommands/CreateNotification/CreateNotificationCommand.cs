using MediatR;
using NotificationsService.Application.DTOs.NotificationsDto;
using NotificationsService.Domain.Models;

namespace NotificationsService.Application.UseCases.Commands.NotificationCommands.CreateNotification;

public record CreateNotificationCommand : IRequest<Notification>
{
    public NotificationRequestDto NotificationDto { get; set; }
}