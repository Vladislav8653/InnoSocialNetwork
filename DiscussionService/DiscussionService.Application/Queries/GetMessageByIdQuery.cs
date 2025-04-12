using DiscussionService.Application.DTOs;
using MediatR;
using MongoDB.Bson;

namespace DiscussionService.Application.Queries;

public record GetMessageByIdQuery : IRequest<MessageResponseDto>
{
    public ObjectId Id { get; init; }
}