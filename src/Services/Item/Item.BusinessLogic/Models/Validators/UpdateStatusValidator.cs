using FluentValidation;
using Item.BusinessLogic.Models.DTOs;

namespace Item.BusinessLogic.Models.Validators;

public class UpdateStatusValidator : AbstractValidator<UpdateStatusDto>
{
    public UpdateStatusValidator()
    {
        RuleFor(c => c.NormalizedName)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(50);
    }
}