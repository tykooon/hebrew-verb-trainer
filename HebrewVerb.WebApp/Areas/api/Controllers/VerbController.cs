using HebrewVerb.Application.Feature.VerbCards.Queries;
using HebrewVerb.Application.Feature.Verbs.Commands;
using HebrewVerb.Application.Feature.Verbs.Queries;
using HebrewVerb.Application.Interfaces;
using HebrewVerb.Application.Models;
using HebrewVerb.WebApp.Contracts;
using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace HebrewVerb.WebApp.Areas.api.Controllers;

[Route("api/[controller]")]
public class VerbController : BaseApiController
{
    public VerbController(IUnitOfWork unitOfWork, IMediator mediator) : base(unitOfWork, mediator)
    { }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetFullInfo(int id)
    {
        var query = new GetVerbByIdQuery(id);
        var res = await _mediator.Send(query);
        return res.IsSuccess ? Ok(res) : BadRequest(string.Join(", ", res.Errors));
    }

    [HttpGet]
    [Route("fromUri")]
    public async Task<IActionResult> GetVerbFromUri(string uri, bool passive = false)
    {
        var query = new GetVerbFromUriQuery(uri, passive);
        var res = await _mediator.Send(query);
        return Ok(res);
    }


    [HttpGet]
    [Route("{id}/inf")]
    public IActionResult GetInfinitive(int id)
    {
        var v = _unitOfWork.VerbRepository.GetById(id);
        //_logger.LogInformation("Getting infinitive with id = " + id);

        return v == null ?
            NotFound() :
            Ok(new{
                VerbForm = v.Infinitive.HebrewNiqqud,
                Transcription = v.Infinitive.TranscriptionRus
            });
    }

    [HttpPost]
    [Route("addFromUri")]
    public async Task<IActionResult> AddFromUri([FromBody] AddVerbFromUriRequest request)
    {
        var query = new GetVerbFromUriQuery(request.Url, request.IsPassive);
        var res = await _mediator.Send(query);

        if (res == null)
        {
            return BadRequest("Unable to get verb forms from URL");
        }

        var command = new AddNewVerbCommand(res);
        var addRes = await _mediator.Send(command);

        return addRes.IsSuccess ? Ok(addRes) : BadRequest(string.Join(", ", addRes.Errors));
    }

    [HttpPost]
    [Route("updateFromUri")]
    public async Task<IActionResult> UpdateFromUri([FromBody] AddVerbFromUriRequest request)
    {
        var query = new GetVerbFromUriQuery(request.Url, request.IsPassive);
        var res = await _mediator.Send(query);

        if (res == null)
        {
            return BadRequest("Unable to get verb forms from URL");
        }

        var command = new UpdateVerbCommand(res);
        var updateRes = await _mediator.Send(command);

        return updateRes.IsSuccess ? Ok(updateRes) : BadRequest(string.Join(", ", updateRes.Errors));
    }

    [HttpGet]
    [Route("getCards")]
    public async Task<IActionResult> GetFilteredVerbCards(
        [FromQuery] int take,
        [FromQuery] IEnumerable<string> zmans,
        [FromQuery] IEnumerable<string> binyans,
        [FromQuery] IEnumerable<string> gizras,
        [FromQuery] IEnumerable<string> verbModels)
    {
        var filter = Filter.FromParams("", binyans, gizras, verbModels, zmans, take);
        var query = new GetTrainingSetByFilterQuery(filter);
        var res = await _mediator.Send(query);

        return Ok(res);
    }

}
