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
            .FilterRepository
            .FindAllBy(x => x.AppUser.Id == request.UserId && x.FilterName == request.FilterName)
            .SingleOrDefault();

        return result == null 
            ? Task.FromResult(new Filter()) 
            : Task.FromResult(result.Filter);
    }
}
