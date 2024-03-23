using HebrewVerb.Application.Common.Mappers;
using HebrewVerb.Application.Feature.Abstractions;
using HebrewVerb.Application.Interfaces;
using HebrewVerb.Application.Models;
using HebrewVerb.Domain.Entities;
using HebrewVerb.Domain.Interfaces;
using HebrewVerb.SharedKernel.Enums;
using HebrewVerb.SharedKernel.Extensions;
using MediatR;

namespace HebrewVerb.Application.Feature.VerbCards.Queries;

public record GetTrainingSetByFilterQuery(Filter Filter, Language Lang = Language.Russian) :
    IRequest<TrainingVerbSet>
{
}


public class GetTrainingSetByFilterQueryHandler(IUnitOfWork unitOfWork) :
    BaseRequestHandler(unitOfWork),
    IRequestHandler<GetTrainingSetByFilterQuery, TrainingVerbSet>
{
    private readonly Random _random = new(DateTime.UtcNow.Microsecond);

    public async Task<TrainingVerbSet> Handle(GetTrainingSetByFilterQuery request, CancellationToken cancellationToken)
    {
        var filter = request.Filter;
        var zmans = filter.Zmans.GetZmans();
        var filteredVerbs = await _unitOfWork.VerbRepository.GetFilteredVerbs(filter, request.Filter.VerbLimit);

        var result = new TrainingVerbSet()
        {
            MaxLimit = request.Filter.VerbLimit,
            Filter = filter,
            Verbs = filteredVerbs.ToDictionary(v => v.Id, v => v.ToVerbInfo(request.Lang)),
            FormCards = GetAllVerbForms(filteredVerbs, zmans, request.Lang).OrderBy(x => _random.Next())
        };

        return result;
    }

    internal List<VerbFormCard> GetAllVerbForms(IEnumerable<Verb> verbs, IEnumerable<Zman> zmans, Language lang = Language.Russian)
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
                    var cards = FillVerbFormCards(verb.Id, tense, zman, lang);
                    list.AddRange(cards);
                }
            }
        }
        return list;
    }

    internal static IEnumerable<VerbFormCard> FillVerbFormCards(int verbId, IConjugation tense, Zman zman, Language lang = Language.Russian)
    {
        foreach (Guf guf in Guf.All(zman != Zman.Present))
        {
            var wf = tense.Conjugate(guf);
            if (wf != null)
            {
                (string Text, int Stress) = lang switch
                {
                    Language.Russian => (wf?.TranscriptionRus ?? "", wf?.StressLetterRus ?? 0),
                    _ => (wf?.TranscriptionEng ?? "", wf?.StressLetterEng ?? 0)
                };

                yield return new VerbFormCard()
                {
                    VerbId = verbId,
                    Zman = zman.NameHebrew,
                    Guf = guf.NameHebrew,
                    VerbFormHebrew = wf?.Hebrew ?? "",
                    VerbFormHebrewNikkud = wf?.HebrewNikkud ?? "",
                    VerbFormTranslit = Text,
                    TranslitStress = Stress,
                };
            }
        }

    }
}
