using Ardalis.Result;
using HebrewVerb.Application.Interfaces;
using HebrewVerb.Domain.Entities;
using HebrewVerb.SharedKernel.Enums;
using MediatR;

namespace HebrewVerb.Application.Feature.Translations.Commands;

public record DeleteTranslationCommand (int VerbId, int TranslationId) : IRequest<Result>;

public class DeleteTranslationCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteTranslationCommand, Result>
{
    public IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(DeleteTranslationCommand request, CancellationToken cancellationToken)
    {
        bool exists = _unitOfWork.VerbRepository.GetById(request.VerbId) != null;

        if (!exists)
        {
            return Result.NotFound($"Verb is not found.");
        }

        var translation = _unitOfWork.TranslationRepository.GetById(request.TranslationId);

        if (translation == null)
        {
            return Result.NotFound($"Translation is not found.");
        }

        try
        {
            _unitOfWork.TranslationRepository.Delete(translation);
            await _unitOfWork.CommitAsync();
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.CriticalError(ex.Message);
        }
    }
}
