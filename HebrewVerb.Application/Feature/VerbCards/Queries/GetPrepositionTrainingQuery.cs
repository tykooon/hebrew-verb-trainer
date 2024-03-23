using HebrewVerb.Application.Common.Mappers;
using HebrewVerb.Application.Feature.Abstractions;
using HebrewVerb.Application.Interfaces;
using HebrewVerb.Application.Models;
using HebrewVerb.Domain.Entities;
using HebrewVerb.SharedKernel.Enums;
using HebrewVerb.SharedKernel.Extensions;
using MediatR;

namespace HebrewVerb.Application.Feature.VerbCards.Queries;

public record GetPrepositionTrainingQuery(IEnumerable<int> IdList, int Take, Language Lang = Language.Russian) :
    IRequest<TrainingPrepositionSet>
{
}

public class GetPrepositionTrainingQueryHandler(IUnitOfWork unitOfWork) :
    BaseRequestHandler(unitOfWork),
    IRequestHandler<GetPrepositionTrainingQuery, TrainingPrepositionSet>
{
    public async Task<TrainingPrepositionSet> Handle(GetPrepositionTrainingQuery request, CancellationToken cancellationToken)
    {
        // 1. GetAll
        // 2. If IdList not empty - cut list
        // 3. Get Training Set with cards
        // 4. Shuffle

        var preps = await _unitOfWork.PrepositionRepository.GetAllAsync();
        if(request.IdList != null && request.IdList.Any())
        {
            preps = preps.Where(v => request.IdList.Contains(v.Id)).ToList();
        }

        // Check Take

        var result = new TrainingPrepositionSet()
        {
            MaxLimit = 0,
            //FormCards = GetAllPrepositionForms(preps, request.Lang),
            Prepositions = preps.ToDictionary(p => p.Id, p => p.ToInfo(request.Lang))
        };

        return result;
    }

    private List<VerbFormCard> GetAllVerbForms(IEnumerable<Verb> verbs, IEnumerable<Zman> zmans, Language lang = Language.Russian)
    {
        var list = new List<VerbFormCard>();

        if (!zmans.Any())
        {
            zmans = Zman.List;
        }

        foreach (Verb verb in verbs)
        {
            foreach (Zman zman in zmans)
            {
                var tense = _unitOfWork.VerbRepository.GetTenseByVerbId(verb.Id, zman);
                if (tense != null)
                {
                    var cards = GetTrainingSetByFilterQueryHandler
                        .FillVerbFormCards(verb.Id, tense, zman, lang);
                    list.AddRange(cards);
                }
            }
        }

        Random random = new(DateTime.UtcNow.Millisecond);
        return ShuffleCards(list, random);
    }

    public static List<VerbFormCard> ShuffleCards(List<VerbFormCard> listToShuffle, Random random)
    {
        for (int i = listToShuffle.Count - 1; i > 0; i--)
        {
            var k = random.Next(i + 1);
            var value = listToShuffle[k];
            listToShuffle[k] = listToShuffle[i];
            listToShuffle[i] = value;
        }

        return listToShuffle;

    }
}
