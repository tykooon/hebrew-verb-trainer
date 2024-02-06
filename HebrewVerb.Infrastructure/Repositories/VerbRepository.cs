using HebrewVerb.Application.Common.Mappers;
using HebrewVerb.Application.Interfaces.Repositories;
using HebrewVerb.Application.Models;
using HebrewVerb.Domain.Common;
using HebrewVerb.Domain.Entities;
using HebrewVerb.Domain.Enums;
using HebrewVerb.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HebrewVerb.Infrastructure.Repositories;

public class VerbRepository(AppDbContext context) : Repository<Verb, int>(context), IVerbRepository
{
    private readonly DbSet<VerbModel> _verbModels = context.VerbModels;
    private readonly DbSet<Gizra> _gizras = context.Gizras;
    private readonly DbSet<Past> _pasts = context.Pasts;
    private readonly DbSet<Present> _presents = context.Presents;
    private readonly DbSet<Future> _futures = context.Futures;
    private readonly DbSet<Imperative> _imperatives = context.Imperatives;
    private readonly DbSet<Translation> _translations = context.Translations;

    public IConjugation? GetTenseByVerbId(int verbId, Zman zman) =>
        zman.Name switch
        {
            Constants.Past => GetPastByVerbId(verbId),
            Constants.Present => GetPresentByVerbId(verbId),
            Constants.Future => GetFutureByVerbId(verbId),
            Constants.Imperative => GetImperativeByVerbId(verbId),
            _ => null
        };

    public IEnumerable<Verb> GetFilteredVerbs(Filter filter, int randomTake = 0)
    {
        var binyans = filter.Binyans.ToBinyanList();
        var gizras = _gizras.Where(g => filter.Gizras.Contains(g.Name));
        var verbModels = _verbModels.Where(vm => filter.VerbModels.Contains(vm.Name));

        var filtered = MakeInclusions();

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
            .Include(v => v.Shoresh).ThenInclude(sh => sh.Gizras)
            .Include(v => v.VerbModels)
            .Include(v => v.Translation);
    }

    private Past? GetPastByVerbId(int verbId)
    {
        var pastId = DbSet.Find(verbId)?.PastId;
        return _pasts?
            .Include(p => p.MS1)
            .Include(p => p.MP1)
            .Include(p => p.MS2)
            .Include(p => p.FS2)
            .Include(p => p.MP2)
            .Include(p => p.FP2)
            .Include(p => p.MS3)
            .Include(p => p.FS3)
            .Include(p => p.MP3)
            .FirstOrDefault(p => p.Id == pastId);
    }

    private Present? GetPresentByVerbId(int verbId)
    {
        var presentId = DbSet.Find(verbId)?.PresentId;
        return _presents?
            .Include(p => p.MS)
            .Include(p => p.MP)
            .Include(p => p.FS)
            .Include(p => p.FP)
            .FirstOrDefault(p => p.Id == presentId);
    }

    private Future? GetFutureByVerbId(int verbId)
    {
        var futureId = DbSet.Find(verbId)?.FutureId;
        return _futures?
            .Include(p => p.MS1)
            .Include(p => p.MP1)
            .Include(p => p.MS2)
            .Include(p => p.FS2)
            .Include(p => p.MP2)
            .Include(p => p.MS3)
            .Include(p => p.MP3)
            .FirstOrDefault(p => p.Id == futureId);
    }

    private Imperative? GetImperativeByVerbId(int verbId)
    {
        var imperativeId = DbSet.Find(verbId)?.ImperativeId;
        return _imperatives?
            .Include(p => p.MS)
            .Include(p => p.MP)
            .Include(p => p.FS)
            .FirstOrDefault(p => p.Id == imperativeId);
    }
}
