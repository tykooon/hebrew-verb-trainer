using MediatR;

namespace HebrewVerb.Application.Feature.Gizras.Queries;

public record GetAllGizrasQuery() : IRequest<IEnumerable<GizraDto>>;