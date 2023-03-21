using Application.Contracts.Reports;
using Application.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.Reports;

namespace Presentation.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class ReportController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReportController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost]
    public async Task<ActionResult<DepartmentReportDto>> CreateAsync([FromBody] CreateDepartmentReportModel model)
    {
        var command = new CreateDepartmentReport.Comand(model.DepartmentId, model.ReportTime);
        CreateDepartmentReport.Response response = await _mediator.Send(command, CancellationToken);

        return Ok(response.DepartmentReport);
    }
}
