using Chat.Application.Features.Conversations.Queries.CheckConversationExistence;
using Chat.Application.Features.Conversations.Queries.GetConversation;
using Chat.Application.Features.Conversations.Queries.GetUserConversations;
using Chat.Application.Models.DataTransferObjects.Conversations.Requests;
using Chat.Application.Models.DataTransferObjects.Conversations.Responses;
using Chat.Domain.Entities;
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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Conversation>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet]
    public async Task<IActionResult> GetUserConversations(CancellationToken token)
    {
        var query = new GetUserConversationsQuery();
        var result = await _mediator.Send(query, token);

        return Ok(result);
    }

    // GET: api/<ConversationController>/<id>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetConversationResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetConversation([FromRoute] string id, CancellationToken token)
    {
        var query = new GetConversationQuery(id);
        var result = await _mediator.Send(query, token);

        return Ok(result);
    }

    // GET: api/<ConversationController>/check-existence
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("check-existence")]
    public async Task<IActionResult> CheckConversationExistence([FromQuery] CheckConversationExistenceRequest request, CancellationToken token)
    {
        var query = new CheckConversationExistenceQuery(request);
        var result = await _mediator.Send(query, token);

        return Ok(result);
    }
}