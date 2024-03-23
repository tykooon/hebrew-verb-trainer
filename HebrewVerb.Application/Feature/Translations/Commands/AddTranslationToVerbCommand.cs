using Ardalis.Result;
using HebrewVerb.Application.Common.Mappers;
using HebrewVerb.Application.Interfaces;
using HebrewVerb.Application.Models;
using MediatR;

namespace HebrewVerb.Application.Feature.Translations.Commands;

public record AddTranslationToVerbCommand (int VerdId, TranslationDto Dto) : IRequest<Result>;

public class AddTranslationToVerbCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<AddTranslationToVerbCommand, Result>
{
    public IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(AddTranslationToVerbCommand request, CancellationToken cancellationToken)
    {
        var verb = _unitOfWork.VerbRepository.GetById(request.VerdId);

        if (verb == null)
        {
            return Result.NotFound($"Verb is not found.");
        }

        var ids = request.Dto.Prepositions.ToArray();
        var preps = _unitOfWork.PrepositionRepository
            .GetAll().Where(pr => ids.Contains(pr.Id));

        var translation = request.Dto.ToTranslation([..preps]);

        try
        {
            verb.Translations.Add(translation);
            await _unitOfWork.CommitAsync();
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.CriticalError(ex.Message);
        }
    }
}
