using Item.BusinessLogic.Models.DTOs;
using Item.BusinessLogic.Services.Interfaces;
using Item.DataAccess.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Item.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RegionsController(IRegionService _regionService) 
    : ControllerBase
{
    // GET: api/<RegionsController>
    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var result = await _regionService.GetAllAsync(cancellationToken);

        return Ok(result);
    }

    // GET: api/<RegionsController>/<id>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await _regionService.GetByIdAsync(id, cancellationToken);

        return Ok(result);
    }

    // POST: api/<RegionsController>
    [HttpPost]
    [Authorize(Roles = nameof(Role.Administrator))]
    public async Task<IActionResult> Post([FromBody] RegionDto categoryDto, CancellationToken cancellationToken)
    {
        var result = await _regionService.CreateAsync(categoryDto, cancellationToken);

        return Ok(result);
    }

    // PUT api/<RegionsController>/<id>
    [HttpPut("{id:guid}")]
    [Authorize(Roles = nameof(Role.Administrator))]
    public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] RegionDto categoryDto, CancellationToken cancellationToken)
    {
        var result = await _regionService.UpdateAsync(id, categoryDto, cancellationToken);

        return Ok(result);
    }

    // DELETE api/<RegionsController>/<id>
    [HttpDelete("{id:guid}")]
    [Authorize(Roles = nameof(Role.Administrator))]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await _regionService.DeleteByIdAsync(id, cancellationToken);

        return Ok(result);
    }
}