using HebrewVerb.Application.Common.Mappers;
using HebrewVerb.Application.Feature.Abstractions;
using HebrewVerb.Application.Interfaces;
using MediatR;

namespace HebrewVerb.Application.Feature.Gizras.Queries;

public class GetAllGizrasQueryHandler(IUnitOfWork unitOfWork) : BaseRequestHandler(unitOfWork),
    IRequestHandler<GetAllGizrasQuery, IEnumerable<GizraDto>>
{
    Task<IEnumerable<GizraDto>> IRequestHandler<GetAllGizrasQuery, IEnumerable<GizraDto>>.Handle(GetAllGizrasQuery request, CancellationToken cancellationToken)
    {
        var gizras = _unitOfWork.GizraRepository.GetAll().Select(g => g.ToDto());
        return Task.FromResult(gizras);
    }
}
