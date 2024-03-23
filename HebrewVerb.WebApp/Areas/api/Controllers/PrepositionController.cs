using HebrewVerb.Application.Feature.Prepositions.Commands;
using HebrewVerb.Application.Feature.Prepositions.Queries;
using HebrewVerb.Application.Feature.VerbCards.Queries;
using HebrewVerb.Application.Feature.Verbs.Commands;
using HebrewVerb.Application.Feature.Verbs.Queries;
using HebrewVerb.Application.Interfaces;
using HebrewVerb.Application.Models;
using HebrewVerb.WebApp.Common;
using HebrewVerb.WebApp.Contracts;
using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace HebrewVerb.WebApp.Areas.api.Controllers;

[Route("api/prepositions")]
public class PrepositionController(IUnitOfWork unitOfWork, IMediator mediator) : BaseApiController(unitOfWork, mediator)
{
    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetPrepositionInfos()
    {
        var query = new GetPrepositionsQuery();
        var res = await _mediator.Send(query);

        return res != null ? Ok(res) : BadRequest("Unable to get list of prepositions");
    }

    [HttpGet]
    [Route("getFromUri")]
    public async Task<IActionResult> GetPrepositionFromUri(string Uri)
    {
        var query = new GetPrepositionFromUriQuery(Uri);
        var res = await _mediator.Send(query);

        return res.IsSuccess == true ? Ok(res.Value) : BadRequest(res.Errors);
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> AddVerb([FromBody] AddPrepositionRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (!Enum.TryParse(request.Source, out VerbSource source) || source != VerbSource.Pealim)
        {
            return BadRequest("Unknown of missed Verb Source");
        }

        var query = new GetPrepositionFromUriQuery(request.Url);
        var res = await _mediator.Send(query);

        if (!res.IsSuccess)
        {
            return BadRequest(res.Errors);
        }

        var command = new AddNewPrepositionCommand(res.Value);
        var addRes = await _mediator.Send(command);

        return addRes.IsSuccess ? Ok() : BadRequest(addRes.Errors);
    }
}
