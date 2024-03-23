using Ardalis.Result;
using HebrewVerb.Application.Common.Extensions;
using HebrewVerb.Application.Common.Mappers;
using HebrewVerb.Application.Interfaces;
using HebrewVerb.Domain.Entities;
using HebrewVerb.SharedKernel.Enums;
using static HebrewVerb.Application.Common.Mappers.WordFormMapper;
using MediatR;
using HebrewVerb.Application.Models;

namespace HebrewVerb.Application.Feature.Prepositions.Commands;

public record AddNewPrepositionCommand (PrepositionDto Dto) : IRequest<Result>
{
}

public class AddNewPrepositionCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<AddNewPrepositionCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(AddNewPrepositionCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        Language lang;

        if (!Enum.IsDefined(lang = (Language)dto.LangId))
        {
            return Result.Invalid(new ValidationError("Unknown language identifier."));
        }

        WordForm baseForm = dto.BaseForm.FromDto(lang);

        var sameBaseForm = _unitOfWork.PrepositionRepository
            .FindAllBy(v => v.BaseForm.HebrewNikkud == baseForm.HebrewNikkud);

        if (sameBaseForm.Any())
        {
            return Result.Error("Not added. Preposition already exists. To change it use update method");
        }

        var result = new Preposition(baseForm)
        {
            MS1 = dto.MS1?.FromDto(lang) ?? WordForm.Default,
            MP1 = dto.MP1?.FromDto(lang) ?? WordForm.Default,
            MS2 = dto.MS2?.FromDto(lang) ?? WordForm.Default,
            FS2 = dto.FS2?.FromDto(lang) ?? WordForm.Default,
            MP2 = dto.MP2?.FromDto(lang) ?? WordForm.Default,
            FP2 = dto.FP2?.FromDto(lang) ?? WordForm.Default,
            MS3 = dto.MS3?.FromDto(lang) ?? WordForm.Default,
            FS3 = dto.FS3?.FromDto(lang) ?? WordForm.Default,
            MP3 = dto.MP3?.FromDto(lang) ?? WordForm.Default,
            FP3 = dto.FP3?.FromDto(lang) ?? WordForm.Default,

            TranslateRus = dto.TranslationRus,
            TranslateEng = dto.TranslationEng
        };

        _unitOfWork.PrepositionRepository.Add(result);
        await _unitOfWork.CommitAsync();
        return Result.Success();
    }
}