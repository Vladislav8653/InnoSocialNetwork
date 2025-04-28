using NotificationsService.Domain.Models;

namespace NotificationsService.Application.Contracts.RepositoryContracts;

public interface INotificationRepository
{
    Task<NotificationEvent> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task CreateAsync(NotificationEvent notification, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    Task UpdateHangfireJobIdAsync(Guid id, string jobId, CancellationToken cancellationToken);
}
