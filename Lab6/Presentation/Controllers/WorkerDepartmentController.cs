using Application.Contracts.WorkerDepartments;
using Application.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.WorkerDepartments;

namespace Presentation.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class WorkerDepartmentController : ControllerBase
{
    private readonly IMediator _mediator;

    public WorkerDepartmentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost("~/create")]
    public async Task<ActionResult<WorkerDepartmentDto>> CreateAsync([FromBody] CreateWorkerDepartmentModel model)
    {
        var command = new CreateWorkerDepartment.Comand(model.Name);
        CreateWorkerDepartment.Response response = await _mediator.Send(command, CancellationToken);

        return Ok(response.Department);
    }

    [HttpPost("~/change-director")]
    public async Task<ActionResult> ChangeDirectorAsync([FromBody] ChangeDepartmentDirectorModel model)
    {
        var command = new ChangeDepartmentDirector.Comand(model.DepartmentId, model.DirectorId);

        await _mediator.Send(command, CancellationToken);

        return Ok();
    }
}
