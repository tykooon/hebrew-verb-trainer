using Ardalis.Result;
using MediatR;

namespace HebrewVerb.Application.Feature.VerbModels.Commands;

public record AddNewVerbModelCommand(string Name, string Description) : IRequest<Result>;