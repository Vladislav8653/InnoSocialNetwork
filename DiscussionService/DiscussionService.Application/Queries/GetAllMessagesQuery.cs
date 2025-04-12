using DiscussionService.Application.DTOs;
using DiscussionService.Application.Pagination;
using MediatR;

namespace DiscussionService.Application.Queries;

public record GetAllMessagesQuery : IRequest<PagedResult<MessageResponseDto>>
{
    public Guid TweetId { get; init; }
    public PageParams PageParams { get; init; } = null!;
}