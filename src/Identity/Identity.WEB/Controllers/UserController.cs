using Identity.Application.Features.Users.Queries.GetAllUsers;
using Identity.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Identity.WEB.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(IMediator mediator) : Controller
{
    // GET api/<UserController>
    [Authorize(Roles = nameof(Role.Administrator))]
    [HttpGet]
    public IActionResult GetAllUsers(CancellationToken cancellationToken)
    {
        var result = mediator.Send(new GetAllUsersQuery(), cancellationToken);

        return Ok(result);
    }
}
