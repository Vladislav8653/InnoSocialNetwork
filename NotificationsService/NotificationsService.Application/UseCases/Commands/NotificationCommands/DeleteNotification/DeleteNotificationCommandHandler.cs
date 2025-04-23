using MediatR;
using NotificationsService.Application.Contracts.RepositoryContracts;
using NotificationsService.Application.Contracts.ServicesContracts;
using NotificationsService.Application.CustomExceptions;

namespace NotificationsService.Application.UseCases.Commands.NotificationCommands.DeleteNotification;

public class DeleteNotificationCommandHandler(
    INotificationRepository repository) 
    : IRequestHandler<DeleteNotificationCommand>
{
    public async Task<Unit> Handle(DeleteNotificationCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}