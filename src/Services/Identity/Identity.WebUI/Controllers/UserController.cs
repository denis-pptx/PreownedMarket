﻿using Identity.Application.Features.Users.Commands.UpdateUserRole;
using Identity.Application.Models.DataTransferObjects.Users.Requests;

namespace Identity.WebUI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(IMediator _mediator) : Controller
{
    // GET api/<UserController>
    [Authorize(Roles = nameof(Role.Administrator))]
    [HttpGet]
    public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken)
    {
        var query = new GetAllUsersQuery();
        var users = await _mediator.Send(query, cancellationToken);

        return Ok(users);
    }

    // GET api/<UserController>/<id>
    [Authorize(Roles = nameof(Role.Administrator))]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetUserById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery(id);
        var user = await _mediator.Send(query, cancellationToken);

        return Ok(user);
    }

    // DELETE api/<UserController>/<id>
    [Authorize(Roles = nameof(Role.Administrator))]
    [HttpDelete]
    public async Task<IActionResult> DeleteUserById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteUserByIdCommand(id);    
        await _mediator.Send(command, cancellationToken);

        return Ok();
    }

    // PUT api/<UserController>/role
    [Authorize(Roles = nameof(Role.Administrator))]
    [HttpPut("role")]
    public async Task<IActionResult> UpdateUserRole([FromBody] UpdateUserRoleRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateUserRoleCommand(request);
        await _mediator.Send(command, cancellationToken);

        return Ok();
    }
}