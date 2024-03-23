using Ardalis.Result;
using HebrewVerb.Application.Feature.Translations.Commands;
using HebrewVerb.Application.Feature.Translations.Queries;
using HebrewVerb.Application.Feature.Verbs.Commands;
using HebrewVerb.Application.Feature.Verbs.Queries;
using HebrewVerb.Application.Interfaces;
using HebrewVerb.Application.Models;
using HebrewVerb.SharedKernel.Enums;
using HebrewVerb.WebApp.Common;
using HebrewVerb.WebApp.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;

namespace HebrewVerb.WebApp.Areas.api.Controllers;

[Route("api/verbs")]
public class VerbController(IUnitOfWork unitOfWork, IMediator mediator) : BaseApiController(unitOfWork, mediator)
{
    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetAllVerbsInfo()
    {
        var query = new GetVerbInfosByFilterQuery(Filter.Empty);
        var res = await _mediator.Send(query);
        return Ok(res);
    }

    [HttpGet]
    [Route("{id:int:min(1)}")]
    public async Task<IActionResult> GetVerbById(int id, [FromQuery] VerbByIdRequest request)
    {
        if (request == null)
        {
            return BadRequest(ModelState.Values.SelectMany(v => v.Errors));
        }

        var lang = request.Lang?.ToLanguage();
        if (lang == null)
        {
            return BadRequest("Undefined language.");
        }

        if (!request.AllForms)
        {
            var query = new GetVerbInfoByIdQuery(id, lang.Value);
            var res = await _mediator.Send(query);
            return res.IsSuccess ? Ok(res.Value) : BadRequest(string.Join(", ", res.Errors));
        }
        else
        {
            var query = new GetVerbByIdQuery(id, lang.Value);
            var res = await _mediator.Send(query);
            return res.IsSuccess ? Ok(res.Value) : BadRequest(string.Join(", ", res.Errors));
        }
    }

    [HttpGet]
    [Route("filter")]
    public async Task<IActionResult> GetFilteredVerbs([FromQuery] VerbsByFilterRequest filterData, [FromQuery] bool isFilled = false)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Wrong filter parameters.");
        }

        var lang = filterData.Lang?.ToLanguage();
        if (lang == null)
        {
            return BadRequest("Undefined language.");
        }

        Filter filter = Filter.FromParams(
            filterData.Binyan,
            filterData.GizraId,
            filterData.ModelId,
            filterData.TagId,
            []);

        if (!isFilled)
        {
            var queryLite = new GetVerbInfosByFilterQuery(filter, lang.Value);
            var resLite = await _mediator.Send(queryLite);
            if (filterData.Id.Count != 0)
            {
                resLite = resLite.Where(v => filterData.Id.Contains(v.VerbId));
            }
            return Ok(resLite);
        }
        var query = new GetVerbsByFilterQuery(filter, filterData.Id, lang.Value);
        var res = await _mediator.Send(query);
        if (filterData.Id.Count != 0)
        {
            res = res.Where(v => filterData.Id.Contains(v.Id));
        }
        return Ok(res);
    }


    [HttpGet]
    [Route("{id}/infinitive")]
    public async Task<IActionResult> GetInfinitive(int id)
    {
        var query = new GetVerbInfoByIdQuery(id);
        Result<VerbInfo> res = await _mediator.Send(query);
        if (!res.IsSuccess)
        {
            return BadRequest(string.Join(", ", res.Errors));
        }

        return Ok(new{
            res.Value.Infinitive,
            res.Value.InfinitiveNikkud,
            res.Value.Binyan,
            res.Value.Shoresh,
            res.Value.Translations,
        });
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> AddVerb([FromBody] AddVerbRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (!Enum.TryParse(request.Source, out VerbSource source) || source != VerbSource.Pealim)
        {
            return BadRequest("Unknown of missed Verb Source");
        }

        var query = new GetVerbFromUriQuery(request.Url, request.IsPassive);
        var res = await _mediator.Send(query);

        if (res == null)
        {
            return BadRequest("Unable to get verb forms from URL");
        }

        var command = new AddNewVerbCommand(res);
        var addRes = await _mediator.Send(command);

        return UnwrapResult(addRes);
    }

    [HttpPut]
    [Route("update")]
    public async Task<IActionResult> UpdateFromUri([FromBody] AddVerbRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (!Enum.TryParse(request.Source, out VerbSource source) || source != VerbSource.Pealim)
        {
            return BadRequest("Unknown of missed Verb Source");
        }

        var query = new GetVerbFromUriQuery(request.Url, request.IsPassive);
        var res = await _mediator.Send(query);

        if (res == null)
        {
            return BadRequest("Unable to get verb forms from URL");
        }

        var command = new UpdateVerbCommand(res);
        var updateRes = await _mediator.Send(command);

        return UnwrapResult(updateRes);
    }

    private IActionResult UnwrapResult(Result res) =>
        res.IsSuccess? Ok(res.Value) : BadRequest(string.Join(", ", res.Errors));
}
