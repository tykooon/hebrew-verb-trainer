using HebrewVerb.Application.Common.Mappers;
using HebrewVerb.Application.Interfaces;
using HebrewVerb.Application.Models;
using HebrewVerb.SharedKernel.Enums;
using MediatR;

namespace HebrewVerb.Application.Feature.Verbs.Queries;

public record GetVerbsByFilterQuery(Filter Filter, IEnumerable<int> Ids, Language Lang = Language.Russian) : IRequest<IEnumerable<VerbDto>>;


public class GetVerbsByFilterQueryHandler(IUnitOfWork unitOfWork) :
    IRequestHandler<GetVerbsByFilterQuery, IEnumerable<VerbDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<IEnumerable<VerbDto>> Handle(GetVerbsByFilterQuery request, CancellationToken cancellationToken)
    {
        var allVerbs = await _unitOfWork.VerbRepository.GetFilteredVerbs(request.Filter, 0);

        if(request.Ids.Any())
        {
            allVerbs = allVerbs.Where(v => request.Ids.Contains(v.Id));
        }

        List<VerbDto> resList = [];
        foreach(var id in allVerbs.Select(v => v.Id))
        {
            var verb = await _unitOfWork.VerbRepository.GetVerbFullDataByIdAsync(id);
            if(verb != null)
            {
                resList.Add(verb.ToDto(request.Lang));
            }
        }

        return resList;
    }
}