using Ardalis.Result;
using HebrewVerb.Application.Common.Extensions;
using HebrewVerb.Application.Common.Helpers;
using HebrewVerb.Application.Common.Mappers;
using HebrewVerb.Application.Interfaces;
using HebrewVerb.Application.Models;
using HebrewVerb.Domain.Entities;
using HebrewVerb.SharedKernel.Enums;
using HebrewVerb.SharedKernel.Extensions;
using MediatR;
using static HebrewVerb.Application.Common.Mappers.WordFormMapper;

namespace HebrewVerb.Application.Feature.Verbs.Commands;

public record UpdateVerbCommand (VerbDto VerbDto) : IRequest<Result>
{
}

public class UpdateVerbCommandHandler : IRequestHandler<UpdateVerbCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateVerbCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateVerbCommand request, CancellationToken cancellationToken)
    {
        var dto = request.VerbDto;
        Language lang;

        if (!Enum.IsDefined(lang = (Language)dto.LangId))
        {
            return Result.Invalid(new ValidationError("Unknown language identifier."));
        }

        Binyan binyan = dto.Binyan.ToBinyan();

        WordForm infinitive = dto.Infinitive.FromDto(lang);

        var sameInfinitive = _unitOfWork.VerbRepository
            .FindAllBy(v => v.Infinitive.HebrewNikkud == infinitive.HebrewNikkud);

        var verb = sameInfinitive.Where(v => v.Binyan == binyan).FirstOrDefault();

        if (verb == null)
        {
            return Result.NotFound("Not found. Can not update requested verb");
        }

        var sameShoresh = _unitOfWork.ShoreshRepository
            .FindAllBy(v => v.Short == dto.Shoresh.RemoveNonHebrew(false)).FirstOrDefault();
        if (sameShoresh == null)
        {
            verb.Shoresh = new Shoresh(dto.Shoresh);
        }

        if (dto.HasPresent())
        {
            var present = _unitOfWork.VerbRepository.GetTenseByVerbId(verb.Id, Zman.Present);
            if (present != null)
            {
                foreach (var guf in Guf.SecondPersons())
                {
                    present.Conjugate(guf)?.UpdateFromDto(dto.GetWordFormDto(Zman.Present, guf), lang);
                }
            }
            else
            {
                verb.Present = new Present(
                    dto.PresentMs?.FromDto(lang) ?? WordForm.Default,
                    dto.PresentFs?.FromDto(lang) ?? WordForm.Default,
                    dto.PresentMp?.FromDto(lang) ?? WordForm.Default,
                    dto.PresentFp?.FromDto(lang) ?? WordForm.Default);
            }
        }

        if (dto.HasPast())
        {
            var past = _unitOfWork.VerbRepository.GetTenseByVerbId(verb.Id, Zman.Past);
            if (past != null)
            {
                foreach (var guf in Guf.All())
                {
                    past.Conjugate(guf)?.UpdateFromDto(dto.GetWordFormDto(Zman.Past, guf), lang);
                }
            }
            else
            {
                verb.Past = new Past(
                dto.PastMs1?.FromDto(lang) ?? WordForm.Default,
                dto.PastMp1?.FromDto(lang) ?? WordForm.Default,
                dto.PastMs2?.FromDto(lang) ?? WordForm.Default,
                dto.PastFs2?.FromDto(lang) ?? WordForm.Default,
                dto.PastMp2?.FromDto(lang) ?? WordForm.Default,
                dto.PastFp2?.FromDto(lang) ?? WordForm.Default,
                dto.PastMs3?.FromDto(lang) ?? WordForm.Default,
                dto.PastFs3?.FromDto(lang) ?? WordForm.Default,
                dto.PastMp3?.FromDto(lang) ?? WordForm.Default);
            }

        }

        if (dto.HasFuture())
        {
            var future = _unitOfWork.VerbRepository.GetTenseByVerbId(verb.Id, Zman.Future);
            if (future != null)
            {
                foreach (var guf in Guf.All())
                {
                    future.Conjugate(guf)?.UpdateFromDto(dto.GetWordFormDto(Zman.Future, guf), lang);
                }
            }
            else
            {
                verb.Future = new Future(
                dto.FutureMs1?.FromDto(lang) ?? WordForm.Default,
                dto.FutureMp1?.FromDto(lang) ?? WordForm.Default,
                dto.FutureMs2?.FromDto(lang) ?? WordForm.Default,
                dto.FutureFs2?.FromDto(lang) ?? WordForm.Default,
                dto.FutureMp2?.FromDto(lang) ?? WordForm.Default,
                dto.FutureMs3?.FromDto(lang) ?? WordForm.Default,
                dto.FutureMp3?.FromDto(lang) ?? WordForm.Default);
            }
        }

        if (!binyan.IsPassive && dto.HasImperative())
        {
            var imperative = _unitOfWork.VerbRepository.GetTenseByVerbId(verb.Id, Zman.Imperative);
            if (imperative != null)
            {
                foreach (var guf in Guf.SecondPersons())
                {
                    imperative.Conjugate(guf)?.UpdateFromDto(dto.GetWordFormDto(Zman.Imperative, guf), lang);
                }
            }
            else
            {
                verb.Imperative = new Imperative(
                    dto.ImperativeMs?.FromDto(lang),
                    dto.ImperativeFs?.FromDto(lang),
                    dto.ImperativeMp?.FromDto(lang));
            }
        }

        //if (!string.IsNullOrEmpty(dto.Translation))
        //{
        //    var translation = new Translation(lang, dto.Translation);
        //    verb.Translations.Add(translation);
        //}

        if(dto.Tags.Count != 0)
        {
            verb.Tags = [];
            foreach (var tag in dto.Tags)
            {
                verb.Tags.Add(tag);
            }
        }

        if (dto.Gizras.Count != 0)
        {
            verb.Gizras = [];
            foreach (var gizra in dto.Gizras)
            {
                verb.Gizras.Add(gizra);
            }
        }

        if (dto.VerbModels.Count != 0)
        {
            verb.VerbModels = [];
            foreach (var verbModel in dto.VerbModels)
            {
                verb.VerbModels.Add(verbModel);
            }
        }

        await _unitOfWork.CommitAsync();
        return Result.Success();
    }
}

