using DiscussionService.Domain.Models;
using MediatR;

namespace DiscussionService.Application.Queries;

public record GetAllMessagesQuery : IRequest<IEnumerable<Message>>;