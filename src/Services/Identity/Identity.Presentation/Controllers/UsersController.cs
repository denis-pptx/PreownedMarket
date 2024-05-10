using Identity.Application.Features.Users.Commands.DeleteUser;
using Identity.Application.Features.Users.Commands.UpdateUserRole;
using Identity.Application.Features.Users.Queries.GetAllUsers;
using Identity.Application.Features.Users.Queries.GetUserById;
using Identity.Application.Models.DataTransferObjects.Users.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;

namespace Identity.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(IMediator _mediator) : Controller
{
    // GET api/<UsersController>
    [Authorize(Roles = nameof(Role.Administrator))]
    [HttpGet]
    public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken)
    {
        var query = new GetAllUsersQuery();
        var users = await _mediator.Send(query, cancellationToken);

        return Ok(users);
    }

    // GET api/<UsersController>/<id>
    [Authorize(Roles = nameof(Role.Administrator))]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetUserById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery(id);
        var user = await _mediator.Send(query, cancellationToken);

        return Ok(user);
    }

    // DELETE api/<UsersController>/<id>
    [Authorize(Roles = nameof(Role.Administrator))]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteUserById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteUserByIdCommand(id);    
        await _mediator.Send(command, cancellationToken);

        return Ok();
    }

    // PUT api/<UsersController>/role
    [Authorize(Roles = nameof(Role.Administrator))]
    [HttpPut("role")]
    public async Task<IActionResult> UpdateUserRole([FromBody] UpdateUserRoleRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateUserRoleCommand(request);
        await _mediator.Send(command, cancellationToken);

        return Ok();
    }
}