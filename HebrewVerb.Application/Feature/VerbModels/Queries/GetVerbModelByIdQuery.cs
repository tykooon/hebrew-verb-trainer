using HebrewVerb.Application.Common.Mappers;
using HebrewVerb.Application.Feature.Abstractions;
using HebrewVerb.Application.Interfaces;
using HebrewVerb.Application.Models;
using MediatR;

namespace HebrewVerb.Application.Feature.VerbModels.Queries;

public record GetVerbModelByIdQuery(int VerbModelId) : IRequest<VerbModelDto?>;

public class GetVerbModelByIdQueryHandler(IUnitOfWork unitOfWork) : BaseRequestHandler(unitOfWork),
    IRequestHandler<GetVerbModelByIdQuery, VerbModelDto?>
{
    public Task<VerbModelDto?> Handle(GetVerbModelByIdQuery request, CancellationToken cancellationToken)
    {
        var model = _unitOfWork.VerbModelRepository.GetById(request.VerbModelId)?.ToDto();
        return Task.FromResult(model);
    }
}