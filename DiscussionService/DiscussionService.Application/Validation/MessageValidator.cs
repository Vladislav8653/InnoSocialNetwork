using DiscussionService.Domain.Models;
using FluentValidation;

namespace DiscussionService.Application.Validation;

public class MessageValidator : AbstractValidator<Message>
{
    public MessageValidator()
    {
        RuleFor(m => m.Content)
            .MaximumLength(2048).WithMessage("Content must have a name longer than 2048 characters.")
            .MinimumLength(2).WithMessage("Content must have a name longer than 2 characters.");
    }
}