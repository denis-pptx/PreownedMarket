namespace Identity.WebUI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IMediator _mediator) 
    : ControllerBase
{
    // POST api/<AuthController>/register
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);

        return Ok();
    }

    // POST api/<AuthController>/login
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand command, CancellationToken cancellationToken)
    {
        var loginUserVm = await _mediator.Send(command, cancellationToken);

        return Ok(loginUserVm);
    }

    // POST api/<AuthController>/refresh
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand command, CancellationToken cancellationToken)
    {
        var refreshTokenVm = await _mediator.Send(command, cancellationToken);

        return Ok(refreshTokenVm);
    }
}
