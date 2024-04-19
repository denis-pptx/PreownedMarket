using Chat.Application.Features.Messages.Commands.CreateMessage;
using Chat.Application.Features.Messages.Commands.UpdateMessage;
using Chat.Application.Models.DataTransferObjects.Messages.Requests;
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

    // PUT api/<MessageController>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromRoute] string id, [FromBody] UpdateMessageRequest request, CancellationToken token)
    {
        var command = new UpdateMessageCommand(id, request);
        await _mediator.Send(command, token);

        return Ok();
    }
}