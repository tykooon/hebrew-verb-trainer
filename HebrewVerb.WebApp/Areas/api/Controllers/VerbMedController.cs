using HebrewVerb.Core;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Ardalis.GuardClauses;
using HebrewVerb.Application.Commands;

namespace HebrewVerb.WebApp.Areas.api.Controllers;

[Route("api/[controller]")]
public class VerbMedController : ControllerBase
{
    IMediator _mediatr;

    public VerbMedController(IMediator mediatr)
    {
        Guard.Against.Null(mediatr);
        _mediatr = mediatr;
    }


    [HttpPost]
    [Route("addnew")]
    public async Task<IActionResult> AddJson(Verb verb)
    {
        if(verb == null)
        {
            return BadRequest();
        }

        var key = await _mediatr.Send(new AddVerbCommand(verb));
        return key.Match<IActionResult>(
            ok => Ok(ok.Id),
            err => BadRequest(err.FirstOrDefault()));
    }

}
