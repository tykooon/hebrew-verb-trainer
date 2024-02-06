using MediatR;

namespace HebrewVerb.Application.Feature.Shoreshes.Queries;

public record GetAllShoreshesQuery() : IRequest<IEnumerable<ShoreshDto>>;