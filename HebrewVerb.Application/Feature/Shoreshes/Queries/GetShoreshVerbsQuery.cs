using Ardalis.Result;
using HebrewVerb.Application.Feature.Abstractions;
using HebrewVerb.Application.Feature.Verbs.Queries;
using HebrewVerb.Application.Interfaces;
using HebrewVerb.Application.Models;
using HebrewVerb.SharedKernel.Enums;
using MediatR;

namespace HebrewVerb.Application.Feature.Shoreshes.Queries;

public record GetShoreshVerbsQuery(int ShoreshId, Language Language = Language.Russian) : IRequest<Result<IEnumerable<VerbInfo>>>;

public class GetShoreshVerbsQueryHandler(IUnitOfWork unitOfWork) : BaseRequestHandler(unitOfWork),
    IRequestHandler<GetShoreshVerbsQuery, Result<IEnumerable<VerbInfo>>>
{
    public async Task<Result<IEnumerable<VerbInfo>>> Handle(GetShoreshVerbsQuery request, CancellationToken cancellationToken)
    {
        var shoresh = await _unitOfWork.ShoreshRepository.GetByIdAsync(request.ShoreshId);
        if(shoresh == null)
        {
            return Result.NotFound($"No Shoreshes with id {request.ShoreshId} found.");
        }

        List<VerbInfo> list = [];
        bool success = true;
        var cts = new CancellationTokenSource();
        foreach (var id in shoresh.Verbs.Select(v => v.Id))
        {
            var query = new GetVerbInfoByIdQuery(id, request.Language);
            var verb = await new GetVerbInfoByIdQueryHandler(_unitOfWork).Handle(query, cts.Token);
            if (verb != null)
            {
                list.Add(verb);
            }
            else
            {
                success = false;
            }
        }

        return success ? list : Result<IEnumerable<VerbInfo>>.Success(list, $"Unable to load some verbs for soreshe with id {request.ShoreshId}");
    }
}