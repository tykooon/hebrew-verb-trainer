using Ardalis.Result;
using HebrewVerb.Application.Common.Mappers;
using HebrewVerb.Application.Feature.Abstractions;
using HebrewVerb.Application.Interfaces;
using HebrewVerb.Application.Models;
using HebrewVerb.Domain.Entities;
using HebrewVerb.SharedKernel.Enums;
using MediatR;

namespace HebrewVerb.Application.Feature.Verbs.Queries;

public record GetVerbByIdQuery(int VerbId, Language Lang = Language.Russian) : IRequest<Result<VerbDto>>
{
}

public class GetVerbByIdQueryHandler(IUnitOfWork unitOfWork) : BaseRequestHandler(unitOfWork),
    IRequestHandler<GetVerbByIdQuery, Result<VerbDto>>
{
    public async Task<Result<VerbDto>> Handle(GetVerbByIdQuery request, CancellationToken cancellationToken)
    {
        var verb = await _unitOfWork.VerbRepository.GetVerbFullDataByIdAsync(request.VerbId);

        if (verb == null)
        {
            return Result<VerbDto>.NotFound($"Verb with id {request.VerbId} not found");
        }

        var dto = verb.ToDto(Language.Russian);
        return Result.Success(dto);
    }
}
