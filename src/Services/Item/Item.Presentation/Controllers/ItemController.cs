using Item.BusinessLogic.Models.DTOs;
using Item.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Item.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ItemController(IItemService _itemService) 
    : ControllerBase
{
    // GET: api/<ItemController>
    [HttpGet]
    public async Task<IActionResult> Get(
        [FromQuery] string? searchTerm,
        [FromQuery] Guid? cityId,
        [FromQuery] string? categoryNormalizedName,
        [FromQuery] string? statusNormalizedName,
        [FromQuery] Guid? userId,
        [FromQuery] string? sortColumn,
        [FromQuery] string? sortOrder,
        [FromQuery] int page,
        [FromQuery] int pageSize, 
        CancellationToken cancellationToken)
    {
        var result = await _itemService.GetAsync(
            searchTerm, 
            cityId,
            categoryNormalizedName, 
            statusNormalizedName, 
            userId, 
            sortColumn, 
            sortOrder, 
            page, 
            pageSize, 
            cancellationToken);

        return Ok(result);
    }

    // GET: api/<ItemController>/<id>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await _itemService.GetByIdAsync(id, cancellationToken);
        
        return Ok(result);
    }

    // POST: api/<ItemController>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ItemDto itemDto, CancellationToken cancellationToken)
    {
        var result = await _itemService.CreateAsync(itemDto, cancellationToken);

        return Ok(result);
    }

    // PUT: api/<ItemController>/status/<id>
    [HttpPut("status/{id:guid}")]
    public async Task<IActionResult> ChangeItemStatus(Guid id, [FromBody] UpdateStatusDto updateStatusDto, CancellationToken cancellationToken)
    {
        var result = await _itemService.ChangeStatus(id, updateStatusDto, cancellationToken);

        return Ok(result);
    }

    // PUT api/<ItemController>/<id>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] ItemDto itemDto, CancellationToken cancellationToken)
    {
        var result = await _itemService.UpdateAsync(id, itemDto, cancellationToken);

        return Ok(result);
    }

    // DELETE api/<ItemController>/<id>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await _itemService.DeleteByIdAsync(id, cancellationToken);

        return Ok(result);
    }
}