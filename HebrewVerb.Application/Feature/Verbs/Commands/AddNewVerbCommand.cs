using Ardalis.Result;
using MediatR;

namespace HebrewVerb.Application.Feature.Verbs.Commands;

public record AddNewVerbCommand (VerbDto VerbDto) : IRequest<Result>
{
}
