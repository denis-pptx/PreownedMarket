using Item.BusinessLogic.Models.DTOs;
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

    // POST: api/<RegionController>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] RegionDto categoryDto, CancellationToken cancellationToken)
    {
        var result = await _regionService.CreateAsync(categoryDto, cancellationToken);

        return Ok(result);
    }

    // PUT api/<RegionController>/<id>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] RegionDto categoryDto, CancellationToken cancellationToken)
    {
        var result = await _regionService.UpdateAsync(id, categoryDto, cancellationToken);

        return Ok(result);
    }

    // DELETE api/<RegionController>/<id>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await _regionService.DeleteByIdAsync(id, cancellationToken);

        return Ok(result);
    }
}