using Ardalis.Result;
using MediatR;

namespace HebrewVerb.Application.Feature.Gizras.Commands;

public record AddNewGizraCommand(string Name, string Description) : IRequest<Result>;