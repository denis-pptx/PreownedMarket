using Item.BusinessLogic.Models.DTOs;
using Item.BusinessLogic.Services.Interfaces;
using Item.DataAccess.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Item.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController(ICategoryService _categoryService) 
    : ControllerBase
{
    // GET: api/<CategoriesController>
    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var result = await _categoryService.GetAllAsync(cancellationToken);

        return Ok(result);
    }

    // GET: api/<CategoriesController>/<id>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await _categoryService.GetByIdAsync(id, cancellationToken);

        return Ok(result);
    }

    // POST: api/<CategoriesController>
    [HttpPost]
    [Authorize(Roles = nameof(Role.Administrator))]
    public async Task<IActionResult> Post([FromBody] CategoryDto categoryDto, CancellationToken cancellationToken)
    {
        var result = await _categoryService.CreateAsync(categoryDto, cancellationToken);

        return Ok(result);
    }

    // PUT api/<CategoriesController>/<id>
    [HttpPut("{id:guid}")]
    [Authorize(Roles = nameof(Role.Administrator))]
    public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] CategoryDto categoryDto, CancellationToken cancellationToken)
    {
        var result = await _categoryService.UpdateAsync(id, categoryDto, cancellationToken);

        return Ok(result);
    }

    // DELETE api/<CategoriesController>/<id>
    [HttpDelete("{id:guid}")]
    [Authorize(Roles = nameof(Role.Administrator))]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await _categoryService.DeleteByIdAsync(id, cancellationToken);

        return Ok(result);
    }
}