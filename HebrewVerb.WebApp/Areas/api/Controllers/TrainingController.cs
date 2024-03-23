using HebrewVerb.Application.Feature.VerbCards.Queries;
using HebrewVerb.Application.Feature.Verbs.Commands;
using HebrewVerb.Application.Feature.Verbs.Queries;
using HebrewVerb.Application.Interfaces;
using HebrewVerb.Application.Models;
using HebrewVerb.SharedKernel.Enums;
using HebrewVerb.SharedKernel.Extensions;
using HebrewVerb.WebApp.Contracts;
using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace HebrewVerb.WebApp.Areas.api.Controllers;

[Route("api/trainings")]
public class TrainingController(IUnitOfWork unitOfWork, IMediator mediator) : BaseApiController(unitOfWork, mediator)
{
    [HttpGet]
    [Route("verbs/random")]
    public async Task<IActionResult> TrainingVerbsRandomCards([FromQuery] TrainingRandomRequest request)
    {
        var lang = request.Lang?.ToLanguage();
        if (lang == null)
        {
            return BadRequest("Undefined language.");
        }

        var filter = Filter.FromParams(
            request.Binyan,
            request.GizraId,
            request.ModelId,
            request.TagId,
            request.Zman,
            request.Take);
        var query = new GetTrainingSetByFilterQuery(filter, lang.Value);
        var res = await _mediator.Send(query);

        return Ok(res);
    }

    [HttpGet]
    [Route("verbs/selected")]
    public async Task<IActionResult> TrainingVerbsSelectedCards([FromQuery] TrainingSelectedRequest request)
    {
        var lang = request.Lang?.ToLanguage();
        if (lang == null)
        {
            return BadRequest("Undefined language.");
        }

        IEnumerable<Zman> zmans = request.Zman.Count != 0
            ? Zman.List.Where(z=> request.Zman.Contains(z.Name))
            : Zman.MainTenses;
        var query = new GetTrainingSetBySelectionQuery(request.Id, zmans, lang.Value);
        var res = await _mediator.Send(query);
        return Ok(res);
    }

    [HttpGet]
    [Route("prepositions")]
    public async Task<IActionResult> TrainingPrepositionCards([FromQuery] TrainingPrepositionRequest request)
    {
        var lang = request.Lang?.ToLanguage();
        if (lang == null)
        {
            return BadRequest("Undefined language.");
        }

        var query = new GetPrepositionTrainingQuery(request.Id, request.Take, lang.Value);
        var res = await _mediator.Send(query);

        return Ok(res);
    }
}
