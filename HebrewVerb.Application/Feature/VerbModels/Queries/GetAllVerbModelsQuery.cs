using HebrewVerb.Application.Common.Mappers;
using HebrewVerb.Application.Feature.Abstractions;
using HebrewVerb.Application.Interfaces;
using HebrewVerb.Application.Models;
using MediatR;

namespace HebrewVerb.Application.Feature.VerbModels.Queries;

public record GetAllVerbModelsQuery() : IRequest<IEnumerable<VerbModelDto>>;

public class GetAllVerbModelsQueryHandler(IUnitOfWork unitOfWork) : BaseRequestHandler(unitOfWork),
    IRequestHandler<GetAllVerbModelsQuery, IEnumerable<VerbModelDto>>
{
    public Task<IEnumerable<VerbModelDto>> Handle(GetAllVerbModelsQuery request, CancellationToken cancellationToken)
    {
        var models = _unitOfWork.VerbModelRepository.GetAll().Select(vm => vm.ToDto());
        return Task.FromResult(models);
    }
}