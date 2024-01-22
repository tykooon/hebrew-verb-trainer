using Ardalis.Result;
using HebrewVerb.Domain.Enums;
using MediatR;

namespace HebrewVerb.Application.Feature.Verbs.Commands;

public record AddNewVerbCommand (VerbDto VerbDto) : IRequest<Result>
{
}
