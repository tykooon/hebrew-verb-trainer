using HebrewVerb.Application.Common.Mappers;
using HebrewVerb.Application.Feature.Abstractions;
using HebrewVerb.Application.Interfaces;
using HebrewVerb.Application.Models;
using MediatR;

namespace HebrewVerb.Application.Feature.Shoreshes.Queries;

public record GetAllShoreshesQuery() : IRequest<IEnumerable<ShoreshDto>>;

public class GetAllShoreshesQueryHandler(IUnitOfWork unitOfWork) : BaseRequestHandler(unitOfWork),
    IRequestHandler<GetAllShoreshesQuery, IEnumerable<ShoreshDto>>
{
    public async Task<IEnumerable<ShoreshDto>> Handle(GetAllShoreshesQuery request, CancellationToken cancellationToken)
    {
        var shoreshes = await _unitOfWork.ShoreshRepository.GetAllAsync(); 
        return shoreshes.Select(sh => sh.ToDto());
    }
}