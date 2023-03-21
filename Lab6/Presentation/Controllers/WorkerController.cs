using Application.Contracts.Workers;
using Application.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.Workers;

namespace Presentation.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class WorkerController : ControllerBase
{
    private readonly IMediator _mediator;

    public WorkerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost]
    public async Task<ActionResult<WorkerDto>> CreateAsync([FromBody] CreateWorkerModel model)
    {
        var command = new CreateWorker.Comand(model.Name, model.SecondName, model.DepartmentId);
        CreateWorker.Response response = await _mediator.Send(command, CancellationToken);

        return Ok(response.Worker);
    }
}
