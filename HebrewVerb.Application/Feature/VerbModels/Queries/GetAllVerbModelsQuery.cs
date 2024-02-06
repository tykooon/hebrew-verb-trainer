using HebrewVerb.Application.Models;
using MediatR;

namespace HebrewVerb.Application.Feature.VerbModels.Queries;

public record GetAllVerbModelsQuery() : IRequest<IEnumerable<VerbModelDto>>;