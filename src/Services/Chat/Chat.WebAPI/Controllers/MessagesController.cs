using Chat.Application.Features.Messages.Commands.CreateMessage;
using Chat.Application.Features.Messages.Commands.DeleteMessage;
using Chat.Application.Features.Messages.Commands.UpdateMessage;
using Chat.Application.Models.DataTransferObjects.Messages.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chat.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class MessagesController(IMediator _mediator) 
    : ControllerBase
{
    // POST api/<MessagesController>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateMessageRequest request, CancellationToken token)
    {
        var command = new CreateMessageCommand(request);
        var response = await _mediator.Send(command, token);

        return Ok(response);
    }

    // PUT api/<MessagesController>/<id>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] UpdateMessageRequest request, CancellationToken token)
    {
        var command = new UpdateMessageCommand(id, request);
        var response = await _mediator.Send(command, token);

        return Ok(response);
    }

    // DELETE api/<MessagesController>/<id>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken token)
    {
        var command = new DeleteMessageCommand(id);
        var response = await _mediator.Send(command, token);

        return Ok(response);
    }
}