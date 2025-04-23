using AutoMapper;
using MediatR;
using NotificationsService.Application.Contracts.RepositoryContracts;
using NotificationsService.Application.DataTransferObjects.NotificationsDto;

namespace NotificationsService.Application.UseCases.Queries.NotificationQueries.GetAllNotifications;

public class GetAllNotificationsQueryHandler(
    INotificationsRepository repository,
    IMapper mapper)
    : IRequestHandler<GetAllNotificationsQuery, IEnumerable<NotificationDto>>
{
    public async Task<IEnumerable<NotificationDto>> Handle(
        GetAllNotificationsQuery request, 
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}