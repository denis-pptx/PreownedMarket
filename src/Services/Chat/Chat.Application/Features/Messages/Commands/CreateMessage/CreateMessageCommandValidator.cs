using FluentValidation;

namespace Chat.Application.Features.Messages.Commands.CreateMessage;

public class CreateMessageCommandValidator 
    : AbstractValidator<CreateMessageCommand>
{
    public CreateMessageCommandValidator()
    {
        RuleFor(command => command.Request.Text)
            .NotEmpty()
            .MaximumLength(500)
            .WithName(nameof(CreateMessageCommand.Request.Text));
    }
}