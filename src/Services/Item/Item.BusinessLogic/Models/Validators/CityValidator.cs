using FluentValidation;
using Item.BusinessLogic.Models.DTOs;

namespace Item.BusinessLogic.Models.Validators;

public class CityValidator : AbstractValidator<CityDto>
{
    public CityValidator()
    {
        RuleFor(city => city.Name)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(50);
    }
}