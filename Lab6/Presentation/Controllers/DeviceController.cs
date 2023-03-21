using Application.Contracts.Devices;
using Application.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.Devices;

namespace Presentation.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class DeviceController : ControllerBase
{
    private readonly IMediator _mediator;

    public DeviceController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost("~/email")]
    public async Task<ActionResult<EmailDeviceDto>> CreateEmailDeviceAsync([FromBody] CreateEmailDeviceModel model)
    {
        var command = new CreateEmailDevice.Comand(model.Login);
        CreateEmailDevice.Response response = await _mediator.Send(command, CancellationToken);

        return Ok(response.Device);
    }

    [HttpPost("~/phone")]
    public async Task<ActionResult<PhoneDeviceDto>> CreatePhoneDeviceAsync([FromBody] CreatePhoneDeviceModel model)
    {
        var command = new CreatePhoneDevice.Comand(model.Number);
        CreatePhoneDevice.Response response = await _mediator.Send(command, CancellationToken);

        return Ok(response.Device);
    }
}
