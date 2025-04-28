using AutoMapper;
using MediatR;
using NotificationsService.Application.Contracts.RepositoryContracts;
using NotificationsService.Application.CustomExceptions;
using NotificationsService.Application.DTOs.NotificationsDto;

namespace NotificationsService.Application.UseCases.Queries.NotificationQueries.GetNotificationById;

public class GetNotificationQueryHandler( 
    INotificationRepository repository,
    IMapper mapper)
    : IRequestHandler<GetNotificationQuery, NotificationRequestDto>
{
    public async Task<NotificationRequestDto> Handle(GetNotificationQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}