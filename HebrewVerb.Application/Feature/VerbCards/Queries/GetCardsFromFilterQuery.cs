using HebrewVerb.Application.Models;
using MediatR;

namespace HebrewVerb.Application.Feature.VerbCards.Queries;

public record GetCardsFromFilterQuery(Filter Filter, int Limit) :
    IRequest<(int, IEnumerable<VerbFormCard>)>
{
}
