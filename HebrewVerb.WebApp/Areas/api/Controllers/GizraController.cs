using HebrewVerb.Application.Feature.Gizras.Commands;
using HebrewVerb.Application.Feature.Gizras.Queries;
using HebrewVerb.Application.Interfaces;
using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace HebrewVerb.WebApp.Areas.api.Controllers;

[Route("api/[controller]")]
public class GizraController : BaseApiController
{
    public GizraController(IUnitOfWork unitOfWork, IMediator mediator) : base(unitOfWork, mediator)
    { }

    [HttpGet]
    [Route("getAll")]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetAllGizrasQuery();
        var res = await _mediator.Send(query);
        return Ok(res);
    }

    [HttpPost]
    [Route("addNew")]
    public async Task<IActionResult> AddNew(string name, string description, params string[] binyanNames)
    {
        var command = new AddNewGizraCommand(name, description, binyanNames);
        var res = await _mediator.Send(command);
        return res.IsSuccess ? Created() : BadRequest(string.Join(", ", res.Errors));
    }

    [HttpPatch]
    [Route("addVerbToGizra")]
    public async Task<IActionResult> AddVerdToGizra(int verbId, string gizraName)
    {
        var command = new AddVerbToGizraCommand(verbId, gizraName);
        var res = await _mediator.Send(command);
        return res.IsSuccess ? Ok(res.SuccessMessage) : BadRequest(string.Join(", ", res.Errors));
    }
}
