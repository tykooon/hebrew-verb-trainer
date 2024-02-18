using HebrewVerb.Application.Common.Mappers;
using HebrewVerb.Application.Interfaces.Repositories;
using HebrewVerb.Application.Models;
using HebrewVerb.Domain.Entities;
using HebrewVerb.Domain.Interfaces;
using HebrewVerb.SharedKernel.Enums;
using HebrewVerb.SharedKernel.Constants;
using Microsoft.EntityFrameworkCore;
using HebrewVerb.SharedKernel.Extensions;

namespace HebrewVerb.Infrastructure.Repositories;

public class VerbRepository(AppDbContext context) : Repository<Verb, int>(context), IVerbRepository
{
    private readonly DbSet<VerbModel> _verbModels = context.VerbModels;
    private readonly DbSet<Gizra> _gizras = context.Gizras;
    private readonly DbSet<Past> _pasts = context.Pasts;
    private readonly DbSet<Present> _presents = context.Presents;
    private readonly DbSet<Future> _futures = context.Futures;
    private readonly DbSet<Imperative> _imperatives = context.Imperatives;
    //private readonly DbSet<Translation> _translations = context.Translations;
    //private readonly DbSet<WordForm> _wordForms = context.WordForms;

    public IConjugation? GetTenseByVerbId(int verbId, Zman zman) =>
        zman.Name switch
        {
            TenseName.Past => GetPastByVerbId(verbId),
            TenseName.Present => GetPresentByVerbId(verbId),
            TenseName.Future => GetFutureByVerbId(verbId),
            TenseName.Imperative => GetImperativeByVerbId(verbId),
            _ => null
        };

    public IEnumerable<Verb> GetFilteredVerbs(Filter filter, int randomTake = 0)
    {
        var binyans = filter.Binyans.GetBinyans();
        var gizras = _gizras.Where(g => filter.Gizras.Contains(g.Name));
        var verbModels = _verbModels.Where(vm => filter.VerbModels.Contains(vm.Name));

        var filtered = MakeInclusions();

        if (gizras.Any())
        {
            filtered = filtered.Where(v => v.Gizras.Intersect(gizras).Any());
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

        if (count == 0)
        {
            return Enumerable.Empty<Verb>();
        }

        if (randomTake == 0 || count <= randomTake)
        {
            return filtered.ToList();
        }

        return filtered.OrderBy(v => EF.Functions.Random()).Take(randomTake).ToList();
    }

    protected override IQueryable<Verb> MakeInclusions()
    {
        return base.MakeInclusions()
            .Include(v => v.Infinitive)
            .Include(v => v.Shoresh)
            .Include(v => v.Gizras)
            .Include(v => v.VerbModels)
            .Include(v => v.Translation);
    }

    private Past? GetPastByVerbId(int verbId)
    {
        var pastId = DbSet.Find(verbId)?.PastId;
        return pastId != null ? _pasts?
            .Include(p => p.MS1)
            .Include(p => p.MP1)
            .Include(p => p.MS2)
            .Include(p => p.FS2)
            .Include(p => p.MP2)
            .Include(p => p.FP2)
            .Include(p => p.MS3)
            .Include(p => p.FS3)
            .Include(p => p.MP3)
            .FirstOrDefault(p => p.Id == pastId)
            : null;
    }

    private Present? GetPresentByVerbId(int verbId)
    {
        var presentId = DbSet.Find(verbId)?.PresentId;
        return presentId != null ? _presents?
            .Include(p => p.MS)
            .Include(p => p.MP)
            .Include(p => p.FS)
            .Include(p => p.FP)
            .FirstOrDefault(p => p.Id == presentId)
            : null;
    }

    private Future? GetFutureByVerbId(int verbId)
    {
        var futureId = DbSet.Find(verbId)?.FutureId;
        return futureId != null ? _futures?
            .Include(p => p.MS1)
            .Include(p => p.MP1)
            .Include(p => p.MS2)
            .Include(p => p.FS2)
            .Include(p => p.MP2)
            .Include(p => p.MS3)
            .Include(p => p.MP3)
            .FirstOrDefault(p => p.Id == futureId)
            : null;
    }

    private Imperative? GetImperativeByVerbId(int verbId)
    {
        var imperativeId = DbSet.Find(verbId)?.ImperativeId;
        return imperativeId != null ? _imperatives?
            .Include(p => p.MS)
            .Include(p => p.MP)
            .Include(p => p.FS)
            .FirstOrDefault(p => p.Id == imperativeId)
            : null;
    }
}
