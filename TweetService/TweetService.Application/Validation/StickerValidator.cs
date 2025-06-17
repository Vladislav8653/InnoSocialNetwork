using FluentValidation;
using TweetService.Domain.Models;

namespace TweetService.Application.Validation;

public class StickerValidator : AbstractValidator<Sticker>
{
    public StickerValidator()
    {
        RuleFor(s => s.Name)
            .MaximumLength(32).WithMessage("Sticker must have a name longer than 32 characters.")
            .MinimumLength(2).WithMessage("Sticker must have a name longer than 2 characters.");
    }
}