using FluentValidation;
using Item.BusinessLogic.Models.DTOs;

namespace Item.BusinessLogic.Models.Validators;

public class RegionValidator : AbstractValidator<RegionDto>
{
    public RegionValidator()
    {
        RuleFor(region => region.Name)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(50);
    }
}