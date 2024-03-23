using Ardalis.Result;
using HebrewVerb.Application.Interfaces;
using HebrewVerb.Domain.Entities;
using HebrewVerb.SharedKernel.Enums;
using MediatR;

namespace HebrewVerb.Application.Feature.Translations.Commands;

public record UpdateTranslationCommand (
    int VerbId,
    int TranslationId,
    string? Main,
    string? Aux,
    IEnumerable<int>? PrepositionIds,
    IEnumerable<VerbTag>? Tags) : IRequest<Result>;

public class UpdateTranslationCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateTranslationCommand, Result>
{
    public IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(UpdateTranslationCommand request, CancellationToken cancellationToken)
    {
        var verb = await _unitOfWork.VerbRepository.GetByIdAsync(request.VerbId);
        if (verb == null)
        {
            return Result.NotFound($"Verb is not found.");
        }

        var translation = verb.Translations.Where(tr => tr.Id == request.TranslationId).FirstOrDefault();
        if(translation == null)
        {
            return Result.NotFound($"Verb has not requested translation.");
        }

        IEnumerable<Preposition>? preps = null;
        var ids = request.PrepositionIds;
        if (ids != null)
        {
            preps = _unitOfWork.PrepositionRepository
                .GetAll().Where(pr => ids.Contains(pr.Id));
        }

        translation.Update(request.Main, request.Aux, preps);

        if(request.Tags != null)
        {
            translation.UpdateTags([.. request.Tags]);
        }

        try
        {
            await _unitOfWork.CommitAsync();
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.CriticalError(ex.Message);
        }
    }
}
