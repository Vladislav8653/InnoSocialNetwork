using System.ComponentModel.DataAnnotations;
using AutoMapper;
using DiscussionService.Application.Contracts;
using DiscussionService.Application.DTOs;
using DiscussionService.Application.Exceptions;
using DiscussionService.Application.Queries;
using MediatR;
using MongoDB.Bson;

namespace DiscussionService.Application.UseCases;

public class GetMessageByTweetIdQueryHandler(
    IMessageRepository messageRepository,
    IMapper mapper) : 
    IRequestHandler<GetMessageByIdQuery, MessageResponseDto>
{
    public async Task<MessageResponseDto> Handle(GetMessageByIdQuery request, CancellationToken cancellationToken)
    {
        if (!ObjectId.TryParse(request.Id, out var messageId))
        {
            throw new ValidationException("Invalid message id");
        }
        
        var message = await messageRepository.GetByIdAsync(messageId, cancellationToken);
        
        var messageDto = mapper.Map<MessageResponseDto>(message);
        
        return messageDto;
    }
}