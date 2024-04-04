using FluentValidation;
using Item.BusinessLogic.Models.DTOs;

namespace Item.BusinessLogic.Models.Validators;

public class ItemValidator : AbstractValidator<ItemDto>
{
    public ItemValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MinimumLength(5)
            .MaximumLength(100);

        RuleFor(x => x.Description)
            .MaximumLength(1000);

        RuleFor(x => x.Price)
            .InclusiveBetween(0, 1_000_000);

        RuleFor(x => x.CategoryId)
            .NotEmpty();
    }
}
