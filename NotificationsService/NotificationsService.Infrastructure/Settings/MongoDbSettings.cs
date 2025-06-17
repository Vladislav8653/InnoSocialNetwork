namespace NotificationsService.Infrastructure.Settings;

public record MongoDbSettings
{
    public string ConnectionString { get; init; }
    public string DatabaseName { get; init; }
    public string MessageDocument { get; init; }
    public string HangfireDocument { get; init; }
}