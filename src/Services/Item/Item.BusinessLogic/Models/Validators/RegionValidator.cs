using FluentValidation;
using Item.BusinessLogic.Models.DTOs;

namespace Item.BusinessLogic.Models.Validators;

public class RegionValidator : AbstractValidator<RegionDto>
{
    public RegionValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(50);
    }
}
