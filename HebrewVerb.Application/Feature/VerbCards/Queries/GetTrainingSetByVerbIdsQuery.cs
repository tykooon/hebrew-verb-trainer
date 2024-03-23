using HebrewVerb.Application.Common.Mappers;
using HebrewVerb.Application.Feature.Abstractions;
using HebrewVerb.Application.Interfaces;
using HebrewVerb.Application.Models;
using HebrewVerb.Domain.Entities;
using HebrewVerb.SharedKernel.Enums;
using HebrewVerb.SharedKernel.Extensions;
using MediatR;
using System.Collections.Generic;

namespace HebrewVerb.Application.Feature.VerbCards.Queries;

public record GetTrainingSetBySelectionQuery(IEnumerable<int> IdList, IEnumerable<Zman> Zmans, Language Lang = Language.Russian) :
    IRequest<TrainingVerbSet>
{
}

public class GetTrainingSetBySelectionQueryHandler(IUnitOfWork unitOfWork) :
    BaseRequestHandler(unitOfWork),
    IRequestHandler<GetTrainingSetBySelectionQuery, TrainingVerbSet>
{
    public Task<TrainingVerbSet> Handle(GetTrainingSetBySelectionQuery request, CancellationToken cancellationToken)
    {
        var verbs = _unitOfWork.VerbRepository.FindAllBy(v => request.IdList.Contains(v.Id));
        var filter = new Filter() { Zmans = request.Zmans.GetTagNames(Language.English).ToHashSet() };


        var result = new TrainingVerbSet()
        {
            MaxLimit = 0,
            Filter = filter,
            Verbs = verbs.ToDictionary(v => v.Id, v => v.ToVerbInfo(request.Lang)),
            FormCards = GetAllVerbForms(verbs, request.Zmans, request.Lang)
        };

        return Task.FromResult(result);
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
