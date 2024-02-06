using HebrewVerb.Application.Common.Enums;
using HebrewVerb.Application.Models;
using MediatR;

namespace HebrewVerb.Application.Feature.VerbCards.Queries;

public record GetTrainingSetByFilterQuery(Filter Filter, int Limit, AppLanguage Lang = AppLanguage.Russian) :
    IRequest<TrainingSet>
{
}
