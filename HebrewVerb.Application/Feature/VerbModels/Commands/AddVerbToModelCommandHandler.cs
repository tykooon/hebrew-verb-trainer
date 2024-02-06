using Ardalis.Result;
using HebrewVerb.Application.Feature.Abstractions;
using HebrewVerb.Application.Interfaces;
using MediatR;

namespace HebrewVerb.Application.Feature.VerbModels.Commands;

public class AddVerbToModelCommandHandler(IUnitOfWork unitOfWork) : 
    BaseRequestHandler(unitOfWork), IRequestHandler<AddVerbToModelCommand, Result>
{
    public async Task<Result> Handle(AddVerbToModelCommand request, CancellationToken cancellationToken)
    {
        var verb = _unitOfWork.VerbRepository.GetById(request.VerbId);
        if (verb == null)
        {
            return Result.NotFound($"Verb with id {request.VerbId} not found");
        }

        var model = _unitOfWork.VerbModelRepository
            .GetAll().Where(vm => vm.Name == request.VerbModelName).SingleOrDefault();
        if (model == null)
        {
            return Result.NotFound($"Verb Model with name {request.VerbModelName} not found");
        }

        if (verb.VerbModels.Select(g => g.Name).Contains(request.VerbModelName))
        {
            return Result.SuccessWithMessage($"Verb already has Model {request.VerbModelName}");
        }

        verb.VerbModels.Add(model);
        await _unitOfWork.CommitAsync();
        return Result.Success();
    }
}
