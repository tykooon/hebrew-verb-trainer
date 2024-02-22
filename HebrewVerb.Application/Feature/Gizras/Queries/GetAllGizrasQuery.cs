using HebrewVerb.Application.Common.Mappers;
using HebrewVerb.Application.Feature.Abstractions;
using HebrewVerb.Application.Interfaces;
using HebrewVerb.Application.Models;
using MediatR;

namespace HebrewVerb.Application.Feature.Gizras.Queries;

public record GetAllGizrasQuery() : IRequest<IEnumerable<GizraDto>>;

public class GetAllGizrasQueryHandler(IUnitOfWork unitOfWork) : BaseRequestHandler(unitOfWork),
    IRequestHandler<GetAllGizrasQuery, IEnumerable<GizraDto>>
{
    public Task<IEnumerable<GizraDto>> Handle(GetAllGizrasQuery request, CancellationToken cancellationToken)
    {
        var gizras = _unitOfWork.GizraRepository.GetAll().Select(g => g.ToDto());
        return Task.FromResult(gizras);
    }
}