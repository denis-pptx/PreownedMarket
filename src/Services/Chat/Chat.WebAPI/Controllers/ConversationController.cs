using Chat.Application.Features.Conversations.Queries.GetUserConversations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Chat.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ConversationController(IMediator _mediator) 
    : ControllerBase
{
    // GET: api/<ConversationController>
    [HttpGet]
    public async Task<IActionResult> GetUserConversations(CancellationToken token)
    {
        var query = new GetUserConversationsQuery();
        var result = await _mediator.Send(query, token);

        return Ok(result);
    }
}