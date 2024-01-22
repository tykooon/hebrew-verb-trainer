using HebrewVerb.Application.Models;
using MediatR;

namespace HebrewVerb.Application.Feature.Verbs.Queries;

public record GetVerbInfosByFilterQuery(Filter Filter) : IRequest<IEnumerable<VerbInfo>>
{
}
