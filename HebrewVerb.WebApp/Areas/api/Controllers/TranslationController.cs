using Ardalis.Result;
using HebrewVerb.Application.Feature.Translations.Commands;
using HebrewVerb.Application.Feature.Translations.Queries;
using HebrewVerb.Application.Interfaces;
using HebrewVerb.Application.Models;
using HebrewVerb.SharedKernel.Enums;
using HebrewVerb.WebApp.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HebrewVerb.WebApp.Areas.api.Controllers;

[Route("api/verbs")]
public class TranslationController(IUnitOfWork unitOfWork, IMediator mediator) : BaseApiController(unitOfWork, mediator)
{
    [HttpGet]
    [Route("{id:int:min(1)}/translations")]
    public async Task<IActionResult> GetVerbTranslations(int id, [FromQuery] string? lang = "rus")
    {
        var language = lang?.ToLanguage();
        if (language == null)
        {
            ModelState.AddModelError("LanguageError", "Undefined Language");
            return BadRequest(ModelState);
        }

        var query = new GetTranslationsByVerbIdQuery(id, language.Value);
        var res = await _mediator.Send(query);
        return res.IsSuccess ? Ok(res.Value) : NotFound(string.Join(", ", res.Errors));
    }

    [HttpPost]
    [Route("{id:int:min(1)}/translations")]
    public async Task<IActionResult> AddVerbTranslations(int id, [FromBody] AddTranslationRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var lang = request.Lang?.ToLanguage();
        if ( lang == null)
        {
            return BadRequest("Undefined language.");
        }

        var tags = VerbTag.List.Where(v => request.Tag.Contains(v.Name.ToLower()));
        var dto = new TranslationDto(0, lang.Value, id, request.Main, request.Aux, tags, request.PrepositionIds);

        var command = new AddTranslationToVerbCommand(id, dto);
        var res = await _mediator.Send(command);
        return UnwrapResult(res);
    }

    [HttpPatch]
    [Route("{id:int:min(1)}/translations")]
    public async Task<IActionResult> UpdateVerbTranslation(int id, [FromBody] UpdateTranslationRequest request)
    {
        var tags = request.Tag != null
            ? VerbTag.List.Where(v => request.Tag.Contains(v.Name.ToLower())) : null;
        var command = new UpdateTranslationCommand(id, request.Id, request.Main, request.Aux, request.PrepositionIds, tags);
        var res = await _mediator.Send(command);
        return UnwrapResult(res);
    }

    [HttpDelete]
    [Route("{id:int:min(1)}/translations")]
    public async Task<IActionResult> DeleteVerbTranslations(int id, [FromQuery] int translationId)
    {
        var command = new DeleteTranslationCommand(id, translationId);
        var res = await _mediator.Send(command);
        return UnwrapResult(res);
    }

    private IActionResult UnwrapResult(Result res) =>
        res.IsSuccess? Ok(res.Value) : BadRequest(string.Join(", ", res.Errors));
}
