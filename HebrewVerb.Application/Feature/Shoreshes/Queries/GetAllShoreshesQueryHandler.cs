using HebrewVerb.Application.Common.Mappers;
using HebrewVerb.Application.Feature.Abstractions;
using HebrewVerb.Application.Interfaces;
using MediatR;

namespace HebrewVerb.Application.Feature.Shoreshes.Queries;

public class GetAllShoreshesQueryHandler(IUnitOfWork unitOfWork) : BaseRequestHandler(unitOfWork),
    IRequestHandler<GetAllShoreshesQuery, IEnumerable<ShoreshDto>>
{
    public Task<IEnumerable<ShoreshDto>> Handle(GetAllShoreshesQuery request, CancellationToken cancellationToken)
    {
        var shoreshes = _unitOfWork.ShoreshRepository.GetAll().Select(sh => sh.ToDto());
        return Task.FromResult(shoreshes);
    }
}
