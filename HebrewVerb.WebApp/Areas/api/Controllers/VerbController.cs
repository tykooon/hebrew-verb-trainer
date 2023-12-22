using HebrewVerb.Application;
using HebrewVerb.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HebrewVerb.WebApp.Areas.api.Controllers;

[Route("api/[controller]")]
public class VerbController : BaseApiController
{
    private ILogger _logger; 

    public VerbController(IUnitOfWork unitOfWork, ILogger<VerbController> logger) : base(unitOfWork)
    {
        _logger = logger;    
    }

    [HttpGet]
    [Route("{id}/fullinfo")]
    public IActionResult GetFullInfo(int id)
    {
        var v = _unitOfWork.VerbRepo.GetById(id);

        return v == null ?  NotFound() : Ok(v);
    }

    [HttpGet]
    [Route("{id}/inf")]
    public IActionResult GetInfinitive(int id)
    {
        var v = _unitOfWork.VerbRepo.GetById(id);
        _logger.LogInformation("Getting infinitive with id = " + id);

        return v == null ?
            NotFound() :
            Ok(new{
                VerbForm = v.Inf,
                Transcription = v.InfT
            });
    }

    [HttpGet]
    [Route("{id}/imp/{gender}/{form}")]
    public IActionResult GetImperative(int id, string gender, string form)
    {
        var v = _unitOfWork.VerbRepo.GetById(id);

        if (v == null)
        {
            return NotFound();
        }

        (string?, string?) response = (gender, form) switch
        {
            ("m", "s") => (v.Imp2MS, v.Imp2MST),
            ("m", "p") => (v.Imp2P, v.Imp2PT),
            ("f", "s") => (v.Imp2FS, v.Imp2FST),
            ("f", "p") => (v.Imp2P, v.Imp2P),
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
    [Route("add-json")]
    public IActionResult AddJson(Verb verb)
    {
        if(verb == null)
        {
            return BadRequest();
        }

        var key = _unitOfWork.VerbRepo.Add(verb);
        return key == null ?
            BadRequest("Value was not added") :
            Ok(new
            {
                Status = "Ok",
                Id = key
            });
    }

}
