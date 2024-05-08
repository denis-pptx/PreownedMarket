using Identity.Application.Abstractions.Messaging;
using Identity.Application.Models.DataTransferObjects.Identity.Requests;
using MediatR;

namespace Identity.Application.Features.Identity.Commands.RegisterUser;

public record RegisterUserCommand(RegisterUserRequest Request)
    : ICommand<Unit>;