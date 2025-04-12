using AutoMapper;
using DiscussionService.Application.Contracts;
using DiscussionService.Application.DTOs;
using DiscussionService.Application.Queries;
using MediatR;

namespace DiscussionService.Application.UseCases;

public class GetMessageByTweetIdQueryHandler(
    IMessageRepository messageRepository,
    IMapper mapper) : 
    IRequestHandler<GetMessageByIdQuery, MessageResponseDto>
{
    public async Task<MessageResponseDto> Handle(GetMessageByIdQuery request, CancellationToken cancellationToken)
    {
        var message = await messageRepository.GetByIdAsync(request.Id);
        
        var messageDto = mapper.Map<MessageResponseDto>(message);
        
        return messageDto;
    }
}