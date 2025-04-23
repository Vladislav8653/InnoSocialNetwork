using AutoMapper;
using FluentValidation;
using MediatR;
using NotificationsService.Application.Contracts.RepositoryContracts;
using NotificationsService.Application.Contracts.ServicesContracts;
using NotificationsService.Domain.Models;

namespace NotificationsService.Application.UseCases.Commands.NotificationCommands.CreateNotification;

public class CreateNotificationCommandHandler(
    INotificationRepository repository,
    IValidator<Notification> validator,
    IMapper mapper) 
    : IRequestHandler<CreateNotificationCommand, Notification>
{
    public async Task<Notification> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}