using HebrewVerb.Application.Feature.Shoreshes.Queries;
using HebrewVerb.Application.Interfaces;
using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace HebrewVerb.WebApp.Areas.api.Controllers;

[Route("api/[controller]")]
public class ShoreshController : BaseApiController
{
    public ShoreshController(IUnitOfWork unitOfWork, IMediator mediator) : base(unitOfWork, mediator)
    { }

    [HttpGet]
    [Route("getAll")]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetAllShoreshesQuery();
        var res = await _mediator.Send(query);
        return Ok(res);
    }

    [HttpGet]
    [Route("{shoreshId:int}")]
    public async Task<IActionResult> GetById(int shoreshId)
    {
        var command = new GetShoreshByIdQuery(shoreshId);
        var res = await _mediator.Send(command);
        return res.IsSuccess ? Ok(res.Value) : NotFound(string.Join(", ", res.Errors));
    }
}
