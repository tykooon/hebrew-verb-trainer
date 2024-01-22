using HebrewVerb.Application.Interfaces;
using HebrewVerb.Application.Models;
using MediatR;

namespace HebrewVerb.Application.Feature.Filters.Queries;

public class GetUserFilterQueryHandler(IUnitOfWork unitOfWork) :
    IRequestHandler<GetUserFilterQuery, Filter>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public Task<Filter> Handle(
        GetUserFilterQuery request,
        CancellationToken cancellationToken)
    {
        var result = _unitOfWork
            .FilterSnapshotRepository
            .FindAllBy(x => x.AppUser.Id == request.UserId);

        if (result == null || !result.Any())
        {
            return Task.FromResult(new Filter());
        }

        return Task.FromResult(Filter.FromJson(result.First().FilterJson));
    }
}
