using Microsoft.AspNetCore.Mvc;
using MediatR;
using Ardalis.GuardClauses;

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
    public IActionResult AddJson()
    {
        return BadRequest("Not implemented");
    }
}
