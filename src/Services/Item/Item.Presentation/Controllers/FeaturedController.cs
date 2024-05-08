using Item.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Item.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class FeaturedController(ILikeService _likeService)
    : ControllerBase
{
    // GET: api/<FeaturedController>
    [HttpGet]
    public async Task<IActionResult> GetLikedItems(CancellationToken token)
    {
        var result = await _likeService.GetLikedItemsAsync(token);

        return Ok(result);
    }

    // POST api/<FeaturedController>/like/<id>
    [HttpPost("like/{itemId:guid}")]
    public async Task<IActionResult> LikeItem([FromRoute] Guid itemId, CancellationToken token)
    {
        await _likeService.LikeItemAsync(itemId, token);

        return Ok();
    }

    // POST api/<FeaturedController>/like/<id>
    [HttpPost("unlike/{itemId:guid}")]
    public async Task<IActionResult> UnlikeItem([FromRoute] Guid itemId, CancellationToken token)
    {
        await _likeService.UnlikeItemAsync(itemId, token);

        return Ok();
    }
}