using DiscussionService.Application.DTOs;
using MediatR;

namespace DiscussionService.Application.Queries;

public record GetAllMessagesQuery : IRequest<IEnumerable<MessageResponseDto>>;