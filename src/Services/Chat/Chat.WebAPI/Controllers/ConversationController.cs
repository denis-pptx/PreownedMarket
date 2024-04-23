using Chat.Application.Features.Conversations.Commands.CreateConversation;
using Chat.Application.Features.Conversations.Commands.DeleteConversation;
using Chat.Application.Features.Conversations.Queries.GetConversation;
using Chat.Application.Features.Conversations.Queries.GetConversatoinByItem;
using Chat.Application.Features.Conversations.Queries.GetUserConversations;
using Chat.Application.Models.DataTransferObjects.Conversations.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chat.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ConversationController(ISender _sender) 
    : ControllerBase
{
    // GET: api/<ConversationController>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet]
    public async Task<IActionResult> GetUserConversations(CancellationToken token)
    {
        var query = new GetAllUserConversationsQuery();
        var result = await _sender.Send(query, token);

        return Ok(result);
    }

    // GET: api/<ConversationController>/<id>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetConversation([FromRoute] Guid id, CancellationToken token)
    {
        var query = new GetConversationQuery(id);
        var result = await _sender.Send(query, token);

        return Ok(result);
    }

    // GET: api/<ConversationController>/item/<id>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("item/{itemId:guid}")]
    public async Task<IActionResult> GetConversationByItem([FromRoute] Guid itemId, CancellationToken token)
    {
        var query = new GetConversationByItemQuery(itemId); 
        var result = await _sender.Send(query, token);

        return Ok(result);
    }

    // POST: api/<ConversationController>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateConversationRequest request, CancellationToken token)
    {
        var command = new CreateConversationCommand(request);
        var result = await _sender.Send(command, token);

        return Ok(result);
    }

    // DELETE: api/<ConversationController>/<id>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken token)
    {
        var command = new DeleteConversationCommand(id);
        await _sender.Send(command, token);

        return Ok();
    }
}