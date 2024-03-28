namespace AuthServer.Api.Controllers;


[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{

    [HttpGet("test")]
    public IActionResult Test()
    {
        var a = HttpContext.User;
        return Ok();
    }
    
    [HttpGet("authorize-test")]
    [Authorize]
    public IActionResult AuthorizedTest()
    {
        return Ok("authorized");
    }
}
