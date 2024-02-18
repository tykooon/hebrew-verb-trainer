using HebrewVerb.Application.Entities;
using HebrewVerb.Application.Models;
using MediatR;

namespace HebrewVerb.Application.Feature.Filters.Queries;

public record GetUserFilterQuery(int UserId, string FilterName = AppFilter.DefaultName) : IRequest<Filter> 
{
}
