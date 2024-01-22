using HebrewVerb.Application.Models;
using MediatR;

namespace HebrewVerb.Application.Feature.Filters.Queries;

public record GetUserFilterQuery(int UserId) : IRequest<Filter> 
{
}
