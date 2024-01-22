using HebrewVerb.Application.Common.Mappers;
using HebrewVerb.Application.Feature.Abstractions;
using HebrewVerb.Application.Interfaces;
using HebrewVerb.Application.Models;
using HebrewVerb.Domain.Common;
using HebrewVerb.Domain.Entities;
using HebrewVerb.Domain.Enums;
using HebrewVerb.Domain.Interfaces;
using MediatR;

namespace HebrewVerb.Application.Feature.VerbCards.Queries;

public class GetCardsFromFilterQueryHandler(IUnitOfWork unitOfWork) : BaseRequestHandler(unitOfWork),
    IRequestHandler<GetCardsFromFilterQuery, (int, IEnumerable<VerbFormCard>)>
{
    public Task<(int, IEnumerable<VerbFormCard>)> Handle(GetCardsFromFilterQuery request, CancellationToken cancellationToken)
    {
        var filter = request.Filter;
        var binyans = filter.Binyans.ToBinyanList();
        var zmans = filter.Tenses.ToZmanList();
        var gizras = _unitOfWork.GizraRepository.FindAllBy(g => filter.Gizras.Contains(g.Name));
        var verbModels = _unitOfWork.VerbModelRepository.FindAllBy(vm => filter.VerbModels.Contains(vm.Name));

        var filtered = _unitOfWork.VerbRepository.GetAll();

        if (gizras.Any())
        {
            filtered = filtered.Where(v => v.Shoresh.Gizras.Intersect(gizras).Any());
        }

        if (binyans.Any())
        {
            filtered = filtered.Where(v => binyans.Contains(v.Binyan));
        }

        if (verbModels.Any())
        {
            filtered = filtered.Where(v => v.VerbModels.Intersect(verbModels).Any());
        }

        var count = filtered.Count();
        var list = new List<VerbFormCard>();

        if (count == 0)
        {
            return Task.FromResult((0, list as IEnumerable<VerbFormCard>));
        }

        if (count <= request.Limit)
        {
            list = GetAllVerbForms(filtered, zmans);
            return Task.FromResult((count, list as IEnumerable<VerbFormCard>));
        }

        var verbs = Random.Shared.GetItems(filtered.ToArray(), request.Limit);
        list = GetAllVerbForms(verbs, zmans);

        return Task.FromResult((request.Limit, list as IEnumerable<VerbFormCard>));
    }

    private List<VerbFormCard> GetAllVerbForms(IEnumerable<Verb> verbs, IEnumerable<Zman> zmans)
    {
        var list = new List<VerbFormCard>();
        foreach (Verb verb in verbs)
        {
            var infinitive = verb.Infinitive.HebrewNiqqud;
            var translate = verb.Translate;
            var gizras = verb.Shoresh.Gizras.Select(g => g.Name);
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
                                Zman = zman.HebrewName,
                                Guf = guf.NameHebrew,
                                Infinitive = infinitive!,
                                VerbFormHebrew = wf?.HebrewNiqqud ?? "",
                                VerbFormTranslit = wf?.TranscriptionRus ?? "",
                                TranslitStress = wf?.StressLetterRus ?? 0,
                                Translation = translate!,
                                Gizras = gizras,
                                VerbModels = verbModels
                            });
                        }
                    }

                }
            }
        }
        return list;
    }
}
