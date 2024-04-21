using Chat.Application.Features.Messages.Commands.CreateMessage;
using FluentValidation;

namespace Chat.Application.Features.Messages.Commands.UpdateMessage;

public class UpdateMessageCommandValidator 
    : AbstractValidator<UpdateMessageCommand>
{
    public UpdateMessageCommandValidator()
    {
        RuleFor(x => x.Request.Text)
            .NotEmpty()
            .MaximumLength(500)
            .WithName(nameof(UpdateMessageCommand.Request.Text));
    }
}