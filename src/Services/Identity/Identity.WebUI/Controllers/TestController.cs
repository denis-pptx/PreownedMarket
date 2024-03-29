namespace AuthServer.Api.Controllers;


[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{

    [HttpGet("test")]
    public IActionResult Test()
    {
        return Ok();
    }
    
    [HttpGet("authorize-test")]
    [Authorize(Roles ="Moderator")]
    public IActionResult AuthorizedTest()
    {
        return Ok("authorized");
    }
}
