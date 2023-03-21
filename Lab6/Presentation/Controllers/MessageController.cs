using Application.Contracts.Messages;
using Application.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.Messages;

namespace Presentation.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class MessageController : ControllerBase
{
    private readonly IMediator _mediator;

    public MessageController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost("~/create-email")]
    public async Task<ActionResult<EmailMessageDto>> CreateEmailMessageAsync([FromBody] CreateEmailMessageModel model)
    {
        var command = new CreateEmailMessage.Comand(
            model.SenderDeviceId,
            model.CatcherDeviceId,
            model.SenderId,
            model.CatcherId,
            model.CreationTime,
            model.Theme,
            model.Body);
        CreateEmailMessage.Response response = await _mediator.Send(command, CancellationToken);

        return Ok(response.Message);
    }

    [HttpPost("~/create-phone")]
    public async Task<ActionResult<PhoneMessageDto>> CreatePhoneMessageAsync([FromBody] CreatePhoneMessageModel model)
    {
        var command = new CreatePhoneMessage.Comand(
            model.SenderDeviceId,
            model.CatcherDeviceId,
            model.SenderId,
            model.CatcherId,
            model.CreationTime,
            model.Body);
        CreatePhoneMessage.Response response = await _mediator.Send(command, CancellationToken);

        return Ok(response.Message);
    }

    [HttpPost("~/handle")]
    public async Task<ActionResult> HandleMessageAsync([FromBody] HandleMessageModel model)
    {
        var command = new HandleMessage.Comand(model.MessageId);

        await _mediator.Send(command, CancellationToken);

        return Ok();
    }

    [HttpGet("~/get-emails")]
    public async Task<ActionResult<IReadOnlyCollection<EmailMessageDto>>> RecieveNewEmailMessagesAsync([FromBody]RecieveNewEmailMessagesModel model)
    {
        var command = new RecieveNewEmailMessages.Comand(model.CatcherDeviceId, model.CatcherId);
        RecieveNewEmailMessages.Response response = await _mediator.Send(command, CancellationToken);

        return Ok(response.Messages);
    }

    [HttpGet("~/get-phones")]
    public async Task<ActionResult<IReadOnlyCollection<PhoneMessageDto>>> RecieveNewPhoneMessagesAsync([FromBody] RecieveNewPhoneMessagesModel model)
    {
        var command = new RecieveNewPhoneMessages.Comand(model.CatcherDeviceId, model.CatcherId);
        RecieveNewPhoneMessages.Response response = await _mediator.Send(command, CancellationToken);

        return Ok(response.Messages);
    }
}
