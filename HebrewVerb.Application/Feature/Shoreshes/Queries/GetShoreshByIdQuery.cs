using Ardalis.Result;
using HebrewVerb.Application.Common.Mappers;
using HebrewVerb.Application.Feature.Abstractions;
using HebrewVerb.Application.Interfaces;
using HebrewVerb.Application.Models;
using MediatR;

namespace HebrewVerb.Application.Feature.Shoreshes.Queries;

public record GetShoreshByIdQuery(int ShoreshId) : IRequest<Result<ShoreshDto>>;

public class GetShoreshByIdQueryHandler(IUnitOfWork unitOfWork) : BaseRequestHandler(unitOfWork),
    IRequestHandler<GetShoreshByIdQuery, Result<ShoreshDto>>
{
    public async Task<Result<ShoreshDto>> Handle(GetShoreshByIdQuery request, CancellationToken cancellationToken)
    {
        var shoresh = await _unitOfWork.ShoreshRepository.GetByIdAsync(request.ShoreshId);
        return shoresh == null
            ? Result.NotFound($"No Shoreshes with id {request.ShoreshId} found.")
            : Result.Success(shoresh.ToDto());
    }
}