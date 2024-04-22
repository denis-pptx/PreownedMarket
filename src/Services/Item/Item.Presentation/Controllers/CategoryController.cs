using Item.BusinessLogic.Models.DTOs;
using Item.BusinessLogic.Services.Interfaces;
using Item.DataAccess.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Item.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController(ICategoryService _categoryService) 
    : ControllerBase
{
    // GET: api/<CategoryController>
    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var result = await _categoryService.GetAsync(cancellationToken);

        return Ok(result);
    }

    // GET: api/<CategoryController>/<id>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await _categoryService.GetByIdAsync(id, cancellationToken);

        return Ok(result);
    }

    // POST: api/<CategoryController>
    [HttpPost]
    [Authorize(Roles = nameof(Role.Administrator))]
    public async Task<IActionResult> Post([FromBody] CategoryDto categoryDto, CancellationToken cancellationToken)
    {
        var result = await _categoryService.CreateAsync(categoryDto, cancellationToken);

        return Ok(result);
    }

    // PUT api/<CategoryController>/<id>
    [HttpPut("{id:guid}")]
    [Authorize(Roles = nameof(Role.Administrator))]
    public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] CategoryDto categoryDto, CancellationToken cancellationToken)
    {
        var result = await _categoryService.UpdateAsync(id, categoryDto, cancellationToken);

        return Ok(result);
    }

    // DELETE api/<CategoryController>/<id>
    [HttpDelete("{id:guid}")]
    [Authorize(Roles = nameof(Role.Administrator))]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await _categoryService.DeleteByIdAsync(id, cancellationToken);

        return Ok(result);
    }
}