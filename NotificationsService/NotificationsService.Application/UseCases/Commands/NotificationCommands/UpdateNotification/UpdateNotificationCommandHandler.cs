using AutoMapper;
using FluentValidation;
using Hangfire;
using MediatR;
using NotificationsService.Application.Contracts.RepositoryContracts;
using NotificationsService.Application.Contracts.ServicesContracts;
using NotificationsService.Application.CustomExceptions;
using NotificationsService.Domain.Models;

namespace NotificationsService.Application.UseCases.Commands.NotificationCommands.UpdateNotification;

public class UpdateNotificationCommandHandler(
    IValidator<Notification> validator,
    IMapper mapper) 
    : IRequestHandler<UpdateNotificationCommand>
{
    public async Task<Unit> Handle(UpdateNotificationCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}