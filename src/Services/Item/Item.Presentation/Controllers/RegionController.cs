using Item.BusinessLogic.Models.DTOs;
using Item.BusinessLogic.Services.Implementations;
using Item.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Item.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RegionController(IRegionService _regionService) 
    : ControllerBase
{
    // GET: api/<RegionController>
    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var result = await _regionService.GetAsync(cancellationToken);

        return Ok(result);
    }

    // GET: api/<RegionController>/<id>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await _regionService.GetByIdAsync(id, cancellationToken);

        return Ok(result);
    }
}
