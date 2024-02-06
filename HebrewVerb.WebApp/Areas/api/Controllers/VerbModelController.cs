using HebrewVerb.Application.Feature.VerbModels.Commands;
using HebrewVerb.Application.Feature.VerbModels.Queries;
using HebrewVerb.Application.Interfaces;
using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace HebrewVerb.WebApp.Areas.api.Controllers;

[Route("api/[controller]")]
public class VerbModelController : BaseApiController
{
    public VerbModelController(IUnitOfWork unitOfWork, IMediator mediator) : base(unitOfWork, mediator)
    { }

    [HttpGet]
    [Route("getAll")]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetAllVerbModelsQuery();
        var res = await _mediator.Send(query);
        return Ok(res);
    }

    [HttpPost]
    [Route("addNew")]
    public async Task<IActionResult> AddNew(string name, string description)
    {
        var command = new AddNewVerbModelCommand(name, description);
        var res = await _mediator.Send(command);
        return res.IsSuccess ? Created() : BadRequest(string.Join(", ", res.Errors));
    }

    [HttpPatch]
    [Route("addVerbToModel")]
    public async Task<IActionResult> AddVerbToVerbModel(int verbId, string modelName)
    {
        var command = new AddVerbToModelCommand(verbId, modelName);
        var res = await _mediator.Send(command);
        return res.IsSuccess ? Ok(res.SuccessMessage) : BadRequest(string.Join(", ", res.Errors));
    }
}
