using Ardalis.Result;
using HebrewVerb.Application.Common.Mappers;
using HebrewVerb.Application.Interfaces;
using HebrewVerb.Application.Models;
using HebrewVerb.Domain.Entities;
using HebrewVerb.SharedKernel.Enums;
using MediatR;

namespace HebrewVerb.Application.Feature.Translations.Queries;

public record GetTranslationsByVerbIdQuery (int VerdId, Language Language) : IRequest<Result<IEnumerable<TranslationDto>>>;

public class GetTranslationsByVerbIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetTranslationsByVerbIdQuery, Result<IEnumerable<TranslationDto>>>
{
    public IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<IEnumerable<TranslationDto>>> Handle(GetTranslationsByVerbIdQuery request, CancellationToken cancellationToken)
    {
        var verb = await _unitOfWork.VerbRepository.GetByIdAsync(request.VerdId);

        if (verb == null)
        {
            return Result<IEnumerable<TranslationDto>>.NotFound($"Verb is not found.");
        }

        IEnumerable<Translation> res = verb.Translations;

        if(request.Language != Language.All)
        {
            res = res.Where(tr => tr.Language == request.Language);
        }

        return Result.Success(res.Select(tr => tr.ToDto()));
    }
}
