using Ardalis.Result;
using HebrewVerb.Application.Common.Helpers;
using HebrewVerb.Application.Interfaces;
using HebrewVerb.Domain.Entities;
using HebrewVerb.Domain.Enums;
using MediatR;
using static HebrewVerb.Application.Common.Mappers.WordFormMapper;

namespace HebrewVerb.Application.Feature.Verbs.Commands;

public class AddNewVerbCommandHandler : IRequestHandler<AddNewVerbCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddNewVerbCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(AddNewVerbCommand request, CancellationToken cancellationToken)
    {
        var dto = request.VerbDto;

        if (!ParseHelpers.TryGetLanguage(dto.LangId, out Lang lang))
        {
            return Result.Invalid(new ValidationError("Unknown language identifier. Must be in {'ru', 'en'}."));
        }

        if (!Binyan.TryFromName(dto.Binyan, true, out Binyan binyan))
        {
            binyan = Binyan.Undefined;
        }

        var infinitive = dto.Infinitive?.FromDto(lang);
        if (infinitive == null)
        {
            return Result.Invalid(new ValidationError("Can not add verb with no infinitive."));
        }

        var sameInfinitive = _unitOfWork.VerbRepository
            .FindAllBy(v => v.Infinitive.HebrewNiqqud == infinitive.HebrewNiqqud);

        if (sameInfinitive.Any() && sameInfinitive.Select(v => v.Binyan).Contains(binyan))
        {
            return Result.Error("Not added. Verb already exists");        
        }

        if (sameInfinitive.Any())
        {
            infinitive = sameInfinitive.First().Infinitive;
        }

        var shoresh = new Shoresh(dto.Shoresh);

        var sameShoresh = _unitOfWork.VerbRepository
            .FindAllBy(v => v.Shoresh.Short == shoresh.Short);

        if (sameShoresh.Any())
        {
            shoresh = sameShoresh.First().Shoresh;
        }

        var present = new Present(
            dto.PresentMs?.FromDto(lang) ?? WordForm.Default,
            dto.PresentFs?.FromDto(lang) ?? WordForm.Default,
            dto.PresentMp?.FromDto(lang) ?? WordForm.Default,
            dto.PresentFp?.FromDto(lang) ?? WordForm.Default);

        var past = new Past(
            dto.PastMs1?.FromDto(lang) ?? WordForm.Default,
            dto.PastMp1?.FromDto(lang) ?? WordForm.Default,
            dto.PastMs2?.FromDto(lang) ?? WordForm.Default,
            dto.PastFs2?.FromDto(lang) ?? WordForm.Default,
            dto.PastMp2?.FromDto(lang) ?? WordForm.Default,
            dto.PastFp2?.FromDto(lang) ?? WordForm.Default,
            dto.PastMs3?.FromDto(lang) ?? WordForm.Default,
            dto.PastFs3?.FromDto(lang) ?? WordForm.Default,
            dto.PastMp3?.FromDto(lang) ?? WordForm.Default);

        var future = new Future(
            dto.FutureMs1?.FromDto(lang),
            dto.FutureMp1?.FromDto(lang) ?? WordForm.Default,
            dto.FutureMs2?.FromDto(lang) ?? WordForm.Default,
            dto.FutureFs2?.FromDto(lang) ?? WordForm.Default,
            dto.FutureMp2?.FromDto(lang) ?? WordForm.Default,
            dto.FutureMs3?.FromDto(lang) ?? WordForm.Default,
            dto.FutureMp3?.FromDto(lang) ?? WordForm.Default);

        Imperative imperative = Imperative.Empty;

        if(binyan != Binyan.Hufal && binyan != Binyan.Pual)
        {
            imperative = new Imperative(
                dto.ImperativeMs?.FromDto(lang) ?? WordForm.Default,
                dto.ImperativeFs?.FromDto(lang) ?? WordForm.Default,
                dto.ImperativeMp?.FromDto(lang) ?? WordForm.Default);
        }

        var result = new Verb(
            binyan,
            shoresh,
            infinitive,
            past,
            present,
            future,
            imperative,
            // TODO  Convertion from int to UseFrequency
            UseFrequency.Undefined,
            dto.Translate,
            dto.ExtraInfo,
            dto.IsArchaic,
            dto.IsLiterary,
            dto.IsSlang);

        _unitOfWork.VerbRepository.Add(result);
        await _unitOfWork.CommitAsync();
        return Result.Success();
    }
}
