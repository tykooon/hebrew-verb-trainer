using Ardalis.Result;
using HebrewVerb.Application.Common.Mappers;
using HebrewVerb.Application.Feature.Abstractions;
using HebrewVerb.Application.Interfaces;
using MediatR;

namespace HebrewVerb.Application.Feature.Shoreshes.Queries;

public class GetShoreshByIdQueryHandler(IUnitOfWork unitOfWork) : BaseRequestHandler(unitOfWork),
    IRequestHandler<GetShoreshByIdQuery, Result<ShoreshDto>>
{
    public Task<Result<ShoreshDto>> Handle(GetShoreshByIdQuery request, CancellationToken cancellationToken)
    {
        var shoresh = _unitOfWork.ShoreshRepository.GetById(request.ShoreshId);
        Result<ShoreshDto> result = shoresh == null 
            ? Result.NotFound($"No Shoreshes with id {request.ShoreshId} found.")
            : Result.Success(shoresh.ToDto());
        return Task.FromResult(result);
    }
}
