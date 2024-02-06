using Ardalis.Result;
using MediatR;

namespace HebrewVerb.Application.Feature.VerbModels.Commands;

public record AddVerbToModelCommand(int VerbId, string VerbModelName) : IRequest<Result>
{
}
