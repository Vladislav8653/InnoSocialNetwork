namespace NotificationsService.Application.Contracts;

public interface IEventHandler<in T>
{
    public Task HandleAsync(T message, CancellationToken cancellationToken);
}