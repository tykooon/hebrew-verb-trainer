using Ardalis.Result;
using MediatR;

namespace HebrewVerb.Application.Feature.Verbs.Queries;

public record GetVerbByIdQuery(int VerbId) : IRequest<Result<VerbDto>>
{
}
