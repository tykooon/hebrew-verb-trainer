using Ardalis.Result;
using HebrewVerb.Application.Feature.Abstractions;
using HebrewVerb.Application.Interfaces;
using MediatR;

namespace HebrewVerb.Application.Feature.Gizras.Commands;

public class AddVerbToGizraCommandHandler(IUnitOfWork unitOfWork) : 
    BaseRequestHandler(unitOfWork), IRequestHandler<AddVerbToGizraCommand, Result>
{
    public async Task<Result> Handle(AddVerbToGizraCommand request, CancellationToken cancellationToken)
    {
        var verb = _unitOfWork.VerbRepository.GetById(request.VerbId);
        if (verb == null)
        {
            return Result.NotFound($"Verb with id {request.VerbId} not found");
        }

        var gizra = _unitOfWork.GizraRepository.GetAll().Where(g => g.Name == request.GizraName).SingleOrDefault();
        if (gizra == null)
        {
            return Result.NotFound($"Gizra with name {request.GizraName} not found");
        }

        var shoresh = _unitOfWork.ShoreshRepository.GetById(verb.ShoreshId);
        if (shoresh == null)
        {
            return Result.CriticalError($"Can not retrieve shoresh for verb with id {request.VerbId}");
        }

        if (shoresh.Gizras.Select(g => g.Name).Contains(request.GizraName))
        {
            return Result.SuccessWithMessage($"Verb already has gizra {request.GizraName}");
        }

        shoresh.Gizras.Add(gizra);
        await _unitOfWork.CommitAsync();
        return Result.Success();
    }
}
