using Ardalis.Result;
using MediatR;

namespace HebrewVerb.Application.Feature.Verbs.Commands;

public record UpdateVerbCommand (VerbDto VerbDto) : IRequest<Result>
{
}
