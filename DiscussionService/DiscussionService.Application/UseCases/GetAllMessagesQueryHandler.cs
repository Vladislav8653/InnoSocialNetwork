using AutoMapper;
using DiscussionService.Application.Contracts;
using DiscussionService.Application.DTOs;
using DiscussionService.Application.Pagination;
using DiscussionService.Application.Queries;
using MediatR;

namespace DiscussionService.Application.UseCases;

public class GetAllMessagesQueryHandler(
    IMessageRepository messageRepository,
    IMapper mapper) : 
    IRequestHandler<GetAllMessagesQuery, PagedResult<MessageResponseDto>>
{
    public async Task<PagedResult<MessageResponseDto>> Handle(GetAllMessagesQuery request, CancellationToken cancellationToken)
    {
        var messages = await messageRepository
            .GetPagedAsync(request.TweetId, request.PageParams, cancellationToken);
        
        var messagesResponseDto = mapper.Map<IEnumerable<MessageResponseDto>>(messages.Items);
        
        return new PagedResult<MessageResponseDto>(messagesResponseDto, messages.Total);
    }
}