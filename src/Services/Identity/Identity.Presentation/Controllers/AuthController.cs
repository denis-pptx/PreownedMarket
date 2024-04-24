using Identity.Application.Models.DataTransferObjects.Identity.Requests;

namespace Identity.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IMediator _mediator) 
    : ControllerBase
{
    // POST api/<AuthController>/register
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest request, CancellationToken cancellationToken)
    {
        var command = new RegisterUserCommand(request);
        await _mediator.Send(command, cancellationToken);

        return Ok();
    }

    // POST api/<AuthController>/login
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserRequest request, CancellationToken cancellationToken)
    {
        var command = new LoginUserCommand(request);
        var response = await _mediator.Send(command, cancellationToken);

        return Ok(response);
    }

    // POST api/<AuthController>/refresh
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        var command = new RefreshTokenCommand(request);
        var response = await _mediator.Send(command, cancellationToken);

        return Ok(response);
    }
}