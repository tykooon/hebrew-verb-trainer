using Ardalis.Result;
using HebrewVerb.Application.Common.Extensions;
using HebrewVerb.Application.Common.Mappers;
using HebrewVerb.Application.Interfaces;
using HebrewVerb.Domain.Entities;
using HebrewVerb.SharedKernel.Enums;
using static HebrewVerb.Application.Common.Mappers.WordFormMapper;
using MediatR;
using HebrewVerb.Application.Models;

namespace HebrewVerb.Application.Feature.Verbs.Commands;

/// <summary>
/// <see cref="IRequest"/> to add verb to database from <see cref="Models.VerbDto"/> object
/// </summary>
/// <param name="VerbDto">Dto odject with full information about verb.</param>
public record AddNewVerbCommand (VerbDto VerbDto) : IRequest<Result>
{
}

/// <summary>
/// Handler for <see cref="AddNewVerbCommand"/>
/// </summary>
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
        Language lang;

        if (!Enum.IsDefined(lang = (Language)dto.LangId))
        {
            return Result.Invalid(new ValidationError("Unknown language identifier."));
        }

        if (!Binyan.TryFromName(dto.Binyan, out Binyan? binyan))
        {
            binyan = Binyan.Undefined;
        }

        WordForm infinitive = dto.Infinitive.FromDto(lang);

        var sameInfinitive = _unitOfWork.VerbRepository
            .FindAllBy(v => v.Infinitive.HebrewNikkud == infinitive.HebrewNikkud);

        if (sameInfinitive.Any() && sameInfinitive.Select(v => v.Binyan).Contains(binyan))
        {
            return Result.Error("Not added. Verb already exists. To change it use update method");
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

        var present = dto.HasPresent()
            ? new Present(
                dto.PresentMs?.FromDto(lang) ?? WordForm.Default,
                dto.PresentFs?.FromDto(lang) ?? WordForm.Default,
                dto.PresentMp?.FromDto(lang) ?? WordForm.Default,
                dto.PresentFp?.FromDto(lang) ?? WordForm.Default)
            : null;

        var past = dto.HasPast()
            ? new Past(
                dto.PastMs1?.FromDto(lang) ?? WordForm.Default,
                dto.PastMp1?.FromDto(lang) ?? WordForm.Default,
                dto.PastMs2?.FromDto(lang) ?? WordForm.Default,
                dto.PastFs2?.FromDto(lang) ?? WordForm.Default,
                dto.PastMp2?.FromDto(lang) ?? WordForm.Default,
                dto.PastFp2?.FromDto(lang) ?? WordForm.Default,
                dto.PastMs3?.FromDto(lang) ?? WordForm.Default,
                dto.PastFs3?.FromDto(lang) ?? WordForm.Default,
                dto.PastMp3?.FromDto(lang) ?? WordForm.Default)
            : null;

        var future = dto.HasFuture()
            ? new Future(
                dto.FutureMs1?.FromDto(lang) ?? WordForm.Default,
                dto.FutureMp1?.FromDto(lang) ?? WordForm.Default,
                dto.FutureMs2?.FromDto(lang) ?? WordForm.Default,
                dto.FutureFs2?.FromDto(lang) ?? WordForm.Default,
                dto.FutureMp2?.FromDto(lang) ?? WordForm.Default,
                dto.FutureMs3?.FromDto(lang) ?? WordForm.Default,
                dto.FutureMp3?.FromDto(lang) ?? WordForm.Default)
            : null;

        var imperative = (!binyan.IsPassive && dto.HasImperative())
            ? new Imperative(
                dto.ImperativeMs?.FromDto(lang),
                dto.ImperativeFs?.FromDto(lang),
                dto.ImperativeMp?.FromDto(lang))
            : null;

        var translation = new Translation(lang, dto.Translation);
       
        var result = new Verb(
            binyan,
            shoresh,
            infinitive,
            past,
            present,
            future,
            imperative,
            [ translation],
            dto.Tags,
            dto.ExtraInfo);

        _unitOfWork.VerbRepository.Add(result);
        await _unitOfWork.CommitAsync();
        return Result.Success();
    }
}