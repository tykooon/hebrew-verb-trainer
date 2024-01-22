using Ardalis.Result;
using MediatR;

namespace HebrewVerb.Application.Feature.Gizras.Commands;

public record AddVerbToGizraCommand(int VerbId, string GizraName) : IRequest<Result>
{
}
