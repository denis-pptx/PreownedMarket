using Identity.Application.Abstractions.Messaging;
using Identity.Application.Models.DataTransferObjects.Users.Requests;
using MediatR;

namespace Identity.Application.Features.Users.Commands.UpdateUserRole;

public record UpdateUserRoleCommand(UpdateUserRoleRequest Request) 
    : ICommand<Unit>;