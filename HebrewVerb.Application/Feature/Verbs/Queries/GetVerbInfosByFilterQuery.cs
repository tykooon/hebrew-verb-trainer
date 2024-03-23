using HebrewVerb.Application.Common.Mappers;
using HebrewVerb.Application.Interfaces;
using HebrewVerb.Application.Models;
using HebrewVerb.SharedKernel.Enums;
using MediatR;

namespace HebrewVerb.Application.Feature.Verbs.Queries;

public record GetVerbInfosByFilterQuery(Filter Filter, Language Lang = Language.Russian) : IRequest<IEnumerable<VerbInfo>>;


public class GetVerbInfosByFilterQueryHandler(IUnitOfWork unitOfWork) :
    IRequestHandler<GetVerbInfosByFilterQuery, IEnumerable<VerbInfo>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<IEnumerable<VerbInfo>> Handle(GetVerbInfosByFilterQuery request, CancellationToken cancellationToken)
    {
        var allVerbs = await _unitOfWork.VerbRepository.GetFilteredVerbs(request.Filter, 0);

        return allVerbs?.Select(v => v.ToVerbInfo(request.Lang)) ?? [];
    }
}