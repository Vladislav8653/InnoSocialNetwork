using AutoMapper;
using MediatR;
using NotificationsService.Application.Contracts.RepositoryContracts;
using NotificationsService.Application.DTOs.NotificationsDto;

namespace NotificationsService.Application.UseCases.Queries.NotificationQueries.GetAllNotifications;

public class GetAllNotificationsQueryHandler(
    INotificationRepository repository,
    IMapper mapper)
    : IRequestHandler<GetAllNotificationsQuery, IEnumerable<NotificationRequestDto>>
{
    public async Task<IEnumerable<NotificationRequestDto>> Handle(
        GetAllNotificationsQuery request, 
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}