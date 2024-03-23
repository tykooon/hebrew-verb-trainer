using HebrewVerb.Application.Feature.Shoreshes.Queries;
using HebrewVerb.Application.Interfaces;
using MediatR;

using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace HebrewVerb.WebApp.Areas.api.Controllers;

[Route("api/shoreshes")]
public class ShoreshController(IUnitOfWork unitOfWork, IMediator mediator) :
    BaseApiController(unitOfWork, mediator)
{
    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetAll()
    {
        var res = await _mediator.Send(new GetAllShoreshesQuery());
        return res == null
            ? NotFound()
            : Ok(res);
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var query = new GetShoreshByIdQuery(id);
        var res = await _mediator.Send(query);
        return res.IsSuccess
            ? Ok(res.Value)
            : NotFound(string.Join(", ", res.Errors));
    }

    [HttpGet]
    [Route("{id:int}/verbs")]
    public async Task<IActionResult> GetShoreshVerbs(int id)
    {
        var query = new GetShoreshVerbsQuery(id);
        var res = await _mediator.Send(query);
        if (!res.IsSuccess)
        {
            NotFound(string.Join(", ", res.Errors));
        }

        return string.IsNullOrEmpty(res.SuccessMessage)
            ? Ok(res.Value)
            : Ok(new { Errors = res.SuccessMessage, Verbs = res.Value });
    }


    [HttpGet]
    [Route("{name:regex(^[[אבגדהוזטחיכךמםנןסעפףצץקרשת]]{{3,4}}$)}")]
    public async Task<IActionResult> GetByName(string name)
    {
        var query = new GetShoreshByNameQuery(name);
        var res = await _mediator.Send(query);
        return res.IsSuccess
            ? Ok(res.Value)
            : NotFound(string.Join(", ", res.Errors));
    }
}
