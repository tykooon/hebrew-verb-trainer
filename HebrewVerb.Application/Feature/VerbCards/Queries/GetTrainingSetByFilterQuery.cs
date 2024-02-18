using HebrewVerb.Application.Models;
using HebrewVerb.SharedKernel.Enums;
using MediatR;

namespace HebrewVerb.Application.Feature.VerbCards.Queries;

public record GetTrainingSetByFilterQuery(Filter Filter, Language Lang = Language.Russian) :
    IRequest<TrainingSet>
{
}
