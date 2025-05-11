using Item.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Item.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StatusesController(IStatusService statusService) 
    : Controller
{
    // GET: api/<StatusesController>
    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var result = await statusService.GetAsync(cancellationToken);

        return Ok(result);
    }

    // GET: api/<StatusesController>/<id>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await statusService.GetByIdAsync(id, cancellationToken);

        return Ok(result);
    }
}