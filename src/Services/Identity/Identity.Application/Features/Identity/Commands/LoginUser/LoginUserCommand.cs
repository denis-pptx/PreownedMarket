using Identity.Application.Abstractions.Messaging;
using Identity.Application.Models.DataTransferObjects.Identity.Requests;
using Identity.Application.Models.DataTransferObjects.Identity.Responses;

namespace Identity.Application.Features.Identity.Commands.LoginUser;

public record LoginUserCommand(LoginUserRequest Request)
    : ICommand<LoginUserResponse>;