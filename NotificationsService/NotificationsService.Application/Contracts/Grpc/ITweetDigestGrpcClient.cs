using NotificationsService.Application.DTOs.DigestDto;

namespace NotificationsService.Application.Contracts.Grpc;

public interface ITweetDigestGrpcClient
{
    Task<List<TweetDigestItemDto>> GetDigestAsync(DateTime from, DateTime to);
}