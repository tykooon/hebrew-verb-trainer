using Ardalis.Result;
using HebrewVerb.Application.Feature.Abstractions;
using HebrewVerb.Application.Interfaces;
using HebrewVerb.Domain.Entities;
using HebrewVerb.SharedKernel.Enums;
using MediatR;

namespace HebrewVerb.Application.Feature.Gizras.Commands;

/// <include file='/Common/Docs/FeatureDocs.xml' path='[gizradoc/@name="AddNewGizraCommand"]'/>
public record AddNewGizraCommand(string Name, string Description, IEnumerable<string> Binyans) : IRequest<Result>;

public class AddNewGizraCommandHandler(IUnitOfWork unitOfWork) : BaseRequestHandler(unitOfWork),
    IRequestHandler<AddNewGizraCommand, Result>
{
    public async Task<Result> Handle(AddNewGizraCommand request, CancellationToken cancellationToken)
    {
        var duplicates = _unitOfWork.GizraRepository.GetAll().Where(g => g.Name == request.Name);
        if (duplicates.Any())
        {
            return Result.Unavailable($"Gizra with name {request.Name} already exists.");
        }

        var binyans = request.Binyans.Select(n => Binyan.FromName(n, true));
        var gizra = new Gizra(request.Name, request.Description, [.. binyans]);
        _unitOfWork.GizraRepository.Add(gizra);
        await _unitOfWork.CommitAsync();
        return Result.Success();
    }
}
