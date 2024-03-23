using HebrewVerb.Application.Common.Mappers;
using HebrewVerb.Application.Interfaces;
using HebrewVerb.Application.Models;
using MediatR;

namespace HebrewVerb.Application.Feature.Prepositions.Queries;

public record GetPrepositionsQuery() : IRequest<IEnumerable<PrepositionInfo>>;

public class GetPrepositionsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetPrepositionsQuery, IEnumerable<PrepositionInfo>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<IEnumerable<PrepositionInfo>> Handle(GetPrepositionsQuery request, CancellationToken cancellationToken)
    {
        var prepositions = await _unitOfWork.PrepositionRepository.GetAllAsync();

        var result = prepositions.Select(pr => pr.ToInfo());

        return result;
    }
}

