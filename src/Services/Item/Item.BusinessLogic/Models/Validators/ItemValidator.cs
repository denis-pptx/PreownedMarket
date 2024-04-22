using FluentValidation;
using Item.BusinessLogic.Models.DTOs;

namespace Item.BusinessLogic.Models.Validators;

public class ItemValidator : AbstractValidator<ItemDto>
{
    public ItemValidator()
    {
        RuleFor(item => item.Title)
            .NotEmpty()
            .MinimumLength(5)
            .MaximumLength(100);

        RuleFor(item => item.Description)
        .MaximumLength(1000);

        RuleFor(item => item.Price)
            .InclusiveBetween(0, 1_000_000);

        RuleFor(item => item.CategoryId)
            .NotEmpty();
    }
}