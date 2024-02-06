using Ardalis.Result;
using HebrewVerb.Application.Feature.Abstractions;
using HebrewVerb.Application.Interfaces;
using HebrewVerb.Domain.Entities;
using MediatR;

namespace HebrewVerb.Application.Feature.VerbModels.Commands;

public class AddNewVerbModelCommandHandler(IUnitOfWork unitOfWork) : BaseRequestHandler(unitOfWork),
    IRequestHandler<AddNewVerbModelCommand, Result>
{
    async Task<Result> IRequestHandler<AddNewVerbModelCommand, Result>.Handle(AddNewVerbModelCommand request, CancellationToken cancellationToken)
    {
        var duplicates = _unitOfWork.VerbModelRepository.GetAll().Where(g => g.Name == request.Name);
        if (duplicates.Any())
        {
            return Result.Unavailable($"Verb Model with name {request.Name} already exists.");
        }

        var verbModel = new VerbModel(request.Name, request.Description);
        _unitOfWork.VerbModelRepository.Add(verbModel);
        await _unitOfWork.CommitAsync();
        return Result.Success();
    }
}
