using MediatR;
using NotificationsService.Application.DTOs.NotificationsDto;

namespace NotificationsService.Application.UseCases.Queries.NotificationQueries.GetAllNotifications;

public record GetAllNotificationsQuery : IRequest<IEnumerable<NotificationRequestDto>>;