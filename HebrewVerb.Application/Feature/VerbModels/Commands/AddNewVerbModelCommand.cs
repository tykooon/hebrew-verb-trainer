using Ardalis.Result;
using HebrewVerb.Application.Feature.Abstractions;
using HebrewVerb.Application.Interfaces;
using HebrewVerb.Domain.Entities;
using HebrewVerb.SharedKernel.Enums;
using MediatR;

namespace HebrewVerb.Application.Feature.VerbModels.Commands;

public record AddNewVerbModelCommand(string Name, string Description, IEnumerable<string> Binyans) : IRequest<Result>;


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

        var binyans = request.Binyans.Select(n => Binyan.FromName(n, true));
        var verbModel = new VerbModel(request.Name, request.Description, [.. binyans]);
        _unitOfWork.VerbModelRepository.Add(verbModel);
        await _unitOfWork.CommitAsync();
        return Result.Success();
    }
}