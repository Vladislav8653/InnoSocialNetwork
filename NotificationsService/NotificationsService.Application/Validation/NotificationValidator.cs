using FluentValidation;
using NotificationsService.Domain.Models;

namespace NotificationsService.Application.Validation;

public class NotificationValidator : AbstractValidator<Notification>
{
    public NotificationValidator()
    {
        
    }
}