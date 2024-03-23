using Ardalis.Result;
using HebrewVerb.Application.Common.Mappers;
using HebrewVerb.Application.Feature.Abstractions;
using HebrewVerb.Application.Interfaces;
using HebrewVerb.Application.Models;
using MediatR;

namespace HebrewVerb.Application.Feature.Shoreshes.Queries;

public record GetShoreshByNameQuery(string ShoreshName) : IRequest<Result<ShoreshDto>>;

public class GetShoreshByNameQueryHandler(IUnitOfWork unitOfWork) : BaseRequestHandler(unitOfWork),
    IRequestHandler<GetShoreshByNameQuery, Result<ShoreshDto>>
{
    public async Task<Result<ShoreshDto>> Handle(GetShoreshByNameQuery request, CancellationToken cancellationToken)
    {
        var shoresh = await _unitOfWork.ShoreshRepository.GetByNameAsync(request.ShoreshName);
        return shoresh == null
            ? Result.NotFound($"No Shoreshes like {request.ShoreshName} found.")
            : Result.Success(shoresh.ToDto());
    }
}