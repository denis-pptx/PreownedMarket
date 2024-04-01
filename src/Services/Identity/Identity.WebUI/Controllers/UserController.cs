using Identity.Application.Features.Users.Commands.DeleteUser;
using Identity.Application.Features.Users.Commands.UpdateUserRole;
using Identity.Application.Features.Users.Queries.GetAllUsers;
using Identity.Application.Features.Users.Queries.GetUserById;
using Identity.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

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
        var users = await _mediator.Send(new GetAllUsersQuery(), cancellationToken);

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

    // DELETE api/<UserController>
    [Authorize(Roles = nameof(Role.Administrator))]
    [HttpDelete]
    public async Task<IActionResult> DeleteUserById([FromBody] DeleteUserByIdCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);

        return Ok();
    }

    // PUT api/<UserController>/role
    [Authorize(Roles = nameof(Role.Administrator))]
    [HttpPut("role")]
    public async Task<IActionResult> UpdateUserRole([FromBody] UpdateUserRoleCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);

        return Ok();
    }
}
