using AutoMapper;
using MediatR;
using NotificationsService.Application.Contracts.RepositoryContracts;
using NotificationsService.Application.CustomExceptions;
using NotificationsService.Application.DataTransferObjects.NotificationsDto;

namespace NotificationsService.Application.UseCases.Queries.NotificationQueries.GetNotificationById;

public class GetNotificationQueryHandler( 
    INotificationRepository repository,
    IMapper mapper)
    : IRequestHandler<GetNotificationQuery, NotificationDto>
{
    public async Task<NotificationDto> Handle(GetNotificationQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}