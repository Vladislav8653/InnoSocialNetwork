using NotificationsService.Domain.Models;

namespace NotificationsService.Application.Contracts.RepositoryContracts;

public interface INotificationRepository
{
    Task<Notification> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task CreateAsync(Notification notification, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    Task UpdateHangfireJobIdAsync(Guid id, string jobId, CancellationToken cancellationToken);
}
