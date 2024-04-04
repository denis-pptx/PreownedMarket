using Item.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Item.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StatusController(IStatusService _statusService) 
    : Controller
{
    // GET: api/<StatusController>
    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var result = await _statusService.GetAsync(cancellationToken);

        return Ok(result);
    }

    // GET: api/<StatusController>/<id>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await _statusService.GetByIdAsync(id, cancellationToken);

        return Ok(result);
    }
}
