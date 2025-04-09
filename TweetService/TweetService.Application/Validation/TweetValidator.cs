using FluentValidation;
using TweetService.Domain.Models;

namespace TweetService.Application.Validation;

public class TweetValidator : AbstractValidator<Tweet>
{
    public TweetValidator()
    {
        RuleFor(t => t.Title)
            .NotEmpty().WithMessage("Tweet title cannot be empty")
            .MaximumLength(100).WithMessage("Tweet title cannot be more than 100 characters");
        
        RuleFor(t => t.Content)
            .MaximumLength(1000).WithMessage("Content cannot be more than 1000 characters");
        
    }
}