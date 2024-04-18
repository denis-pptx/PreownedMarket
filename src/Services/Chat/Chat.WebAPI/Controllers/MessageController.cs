using Chat.Application.Features.Conversations.Commands.CreateMessage;
using Chat.Application.Models.DataTransferObjects.Conversations.Requests;
using Chat.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chat.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class MessageController(IMediator _mediator) 
    : ControllerBase
{
    // POST api/<MessageController>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateMessageRequest request, CancellationToken token)
    {
        var command = new CreateMessageCommand(request);
        await _mediator.Send(command, token);

        return Ok();
    }
}