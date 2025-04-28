using MediatR;
using NotificationsService.Application.DTOs.NotificationsDto;

namespace NotificationsService.Application.UseCases.Queries.NotificationQueries.GetNotificationById;

public record GetNotificationQuery : IRequest<NotificationRequestDto>
{
   public Guid NotificationId { get; init; } 
}