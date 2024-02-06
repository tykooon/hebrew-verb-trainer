using Ardalis.Result;
using MediatR;

namespace HebrewVerb.Application.Feature.Shoreshes.Queries;

public record GetShoreshByIdQuery(int ShoreshId) : IRequest<Result<ShoreshDto>>;