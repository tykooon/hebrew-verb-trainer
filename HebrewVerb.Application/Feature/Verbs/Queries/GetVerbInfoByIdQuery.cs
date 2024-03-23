using Ardalis.Result;
using HebrewVerb.Application.Common.Mappers;
using HebrewVerb.Application.Feature.Abstractions;
using HebrewVerb.Application.Interfaces;
using HebrewVerb.Application.Models;
using HebrewVerb.Domain.Entities;
using HebrewVerb.SharedKernel.Enums;
using MediatR;

namespace HebrewVerb.Application.Feature.Verbs.Queries;

public record GetVerbInfoByIdQuery(int VerbId, Language Lang = Language.Russian) : IRequest<Result<VerbInfo>>
{
}

public class GetVerbInfoByIdQueryHandler(IUnitOfWork unitOfWork) : BaseRequestHandler(unitOfWork),
    IRequestHandler<GetVerbInfoByIdQuery, Result<VerbInfo>>
{
    public async Task<Result<VerbInfo>> Handle(GetVerbInfoByIdQuery request, CancellationToken cancellationToken)
    {
        var verb = await _unitOfWork.VerbRepository.GetByIdAsync(request.VerbId);

        if (verb == null)
        {
            return Result<VerbInfo>.NotFound($"Verb with id {request.VerbId} not found");
        }

        VerbInfo dto = verb.ToVerbInfo(Language.Russian);
        return Result.Success(dto);
    }
}
