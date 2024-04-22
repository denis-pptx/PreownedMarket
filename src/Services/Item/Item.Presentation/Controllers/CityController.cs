using Item.BusinessLogic.Models.DTOs;
using Item.BusinessLogic.Services.Interfaces;
using Item.DataAccess.Models.Enums;
using Microsoft.AspNetCore.Authorization;
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

    // POST: api/<CityController>
    [HttpPost]
    [Authorize(Roles = nameof(Role.Administrator))]
    public async Task<IActionResult> Post([FromBody] CityDto cityDto, CancellationToken cancellationToken)
    {
        var result = await _cityService.CreateAsync(cityDto, cancellationToken);

        return Ok(result);
    }

    // PUT api/<CityController>/<id>
    [HttpPut("{id:guid}")]
    [Authorize(Roles = nameof(Role.Administrator))]
    public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] CityDto cityDto, CancellationToken cancellationToken)
    {
        var result = await _cityService.UpdateAsync(id, cityDto, cancellationToken);

        return Ok(result);
    }

    // DELETE api/<CityController>/<id>
    [HttpDelete("{id:guid}")]
    [Authorize(Roles = nameof(Role.Administrator))]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await _cityService.DeleteByIdAsync(id, cancellationToken);

        return Ok(result);
    }
}