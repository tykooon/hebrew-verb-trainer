using HebrewVerb.Application.Feature.VerbCards.Queries;
using HebrewVerb.Application.Feature.Verbs.Commands;
using HebrewVerb.Application.Feature.Verbs.Queries;
using HebrewVerb.Application.Interfaces;
using HebrewVerb.Application.Models;
using HebrewVerb.Domain.Entities;
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

    [HttpGet]
    [Route("{id}/imp/{gender}/{form}")]
    public IActionResult GetImperative(int id, string gender, string form)
    {
        var v = _unitOfWork.VerbRepository.GetById(id);

        if (v == null)
        {
            return NotFound();
        }

        (string?, string?) response = (gender, form) switch
        {
            ("m", "s") => (v.Imperative.MS.HebrewNiqqud, v.Imperative.MS.TranscriptionRus),
            ("m", "p") => (v.Imperative.MP.HebrewNiqqud, v.Imperative.MP.TranscriptionRus),
            ("f", "s") => (v.Imperative.FS.HebrewNiqqud, v.Imperative.FS.TranscriptionRus),
            ("f", "p") => (v.Imperative.FP.HebrewNiqqud, v.Imperative.FP.TranscriptionRus),
            _ => (null, null)
        };

        if (response.Item1 ==  null || response.Item2 == null)
        {
            return NotFound();
        }

        return Ok(new{
            VerbForm = response.Item1,
            Transcription = response.Item2
        });
    }

    [HttpPost]
    [Route("addFromUri")]
    public async Task<IActionResult> AddFromUri(string url, bool passive = false)
    {
        var query = new GetVerbFromUriQuery(url, passive);
        var res = await _mediator.Send(query);

        if (res == null)
        {
            return BadRequest("Unable to get verb forms from URL");
        }

        var command = new AddNewVerbCommand(res);
        var addRes = await _mediator.Send(command);

        return addRes.IsSuccess ? Ok(addRes) : BadRequest(string.Join(", ", addRes.Errors));
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
        var filter = Filter.FromParams(binyans, zmans, gizras, verbModels);
        var query = new GetCardsFromFilterQuery(filter, take);
        var res = await _mediator.Send(query);
        

        return Ok(new { amount = res.Item1, list = res.Item2 });
    }

}
