using HebrewVerb.Application.Common.Mappers;
using HebrewVerb.Application.Feature.Abstractions;
using HebrewVerb.Application.Interfaces;
using HebrewVerb.Application.Models;
using MediatR;

namespace HebrewVerb.Application.Feature.Gizras.Queries;

public record GetGizraByIdQuery(int Id) : IRequest<GizraDto?>;

public class GetGizraByIdQueryHandler(IUnitOfWork unitOfWork) : BaseRequestHandler(unitOfWork),
    IRequestHandler<GetGizraByIdQuery, GizraDto?>
{
    public Task<GizraDto?> Handle(GetGizraByIdQuery request, CancellationToken cancellationToken)
    {
        var gizra = _unitOfWork.GizraRepository.GetById(request.Id)?.ToDto();
        return Task.FromResult(gizra);
    }
}