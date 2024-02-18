using HebrewVerb.Application.Common.Mappers;
using HebrewVerb.Application.Feature.Abstractions;
using HebrewVerb.Application.Interfaces;
using HebrewVerb.Application.Models;
using HebrewVerb.Domain.Entities;
using HebrewVerb.SharedKernel.Enums;
using HebrewVerb.SharedKernel.Extensions;
using MediatR;

namespace HebrewVerb.Application.Feature.VerbCards.Queries;

public class GetTrainingSetByFilterQueryHandler(IUnitOfWork unitOfWork) :
    BaseRequestHandler(unitOfWork),
    IRequestHandler<GetTrainingSetByFilterQuery, TrainingSet>
{
    public Task<TrainingSet> Handle(GetTrainingSetByFilterQuery request, CancellationToken cancellationToken)
    {
        var filter = request.Filter;
        var zmans = filter.Zmans.GetZmans();
        var filteredVerbs = _unitOfWork.VerbRepository.GetFilteredVerbs(filter, request.Filter.VerbLimit);

        var result = new TrainingSet()
        {
            MaxLimit = request.Filter.VerbLimit,
            Filter = filter,
            Verbs = filteredVerbs.ToDictionary(v => v.Id, v => v.ToVerbInfo(request.Lang)),
            FormCards = GetAllVerbForms(filteredVerbs, zmans, request.Lang)
        };

        return Task.FromResult(result);
    }

    private List<VerbFormCard> GetAllVerbForms(IEnumerable<Verb> verbs, IEnumerable<Zman> zmans, Language lang = Language.Russian)
    {
        var list = new List<VerbFormCard>();
        foreach (Verb verb in verbs)
        {
            var infinitive = verb.Infinitive.HebrewNiqqud;
            var translate = verb.Translation?.Get(lang);
            var gizras = verb.Gizras.Select(g => g.Name);
            var verbModels = verb.VerbModels.Select(vm => vm.Name);

            if (!zmans.Any())
            {
                zmans = Zman.List;
            }

            foreach (Zman zman in zmans)
            {
                var tense = _unitOfWork.VerbRepository.GetTenseByVerbId(verb.Id, zman);
                if (tense != null)
                {
                    foreach (Guf guf in Guf.All(zman != Zman.Present))
                    {
                        var wf = tense.Conjugate(guf);
                        if (wf != null)
                        {
                            list.Add(new VerbFormCard()
                            {
                                VerbId = verb.Id,
                                Zman = zman.NameHebrew,
                                Guf = guf.NameHebrew,
                                VerbFormHebrew = wf?.HebrewNiqqud ?? "",
                                VerbFormTranslit = wf?.TranscriptionRus ?? "",
                                TranslitStress = wf?.StressLetterRus ?? 0,
                            });
                        }
                    }

                }
            }
        }
        return list;
    }
}
