using Item.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Item.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CityController(ICityService _cityService)
    : ControllerBase
{
    // GET: api/<CityController>
    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
       var result = await _cityService.GetAsync(cancellationToken);

        return Ok(result);
    }

    // GET: api/<CityController>/<id>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await _cityService.GetByIdAsync(id, cancellationToken);

        return Ok(result);
    }
}

