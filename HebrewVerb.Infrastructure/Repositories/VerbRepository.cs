using HebrewVerb.Application.Interfaces.Repositories;
using HebrewVerb.Domain.Common;
using HebrewVerb.Domain.Entities;
using HebrewVerb.Domain.Enums;
using HebrewVerb.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HebrewVerb.Infrastructure.Repositories;

public class VerbRepository : Repository<Verb, int>, IVerbRepository
{
    private readonly DbSet<Past> _pasts;
    private readonly DbSet<Present> _presents;
    private readonly DbSet<Future> _futures;
    private readonly DbSet<Imperative> _imperatives;

    public VerbRepository(AppDbContext context) : base(context)
    {
        _pasts = context.Pasts;
        _presents = context.Presents;
        _futures = context.Futures;
        _imperatives = context.Imperatives;
    }

    public IConjugation? GetTenseByVerbId(int verbId, Zman zman)
    {
        return zman.Name switch
        {
            Constants.Past => GetPastByVerbId(verbId),
            Constants.Present => GetPresentByVerbId(verbId),
            Constants.Future => GetFutureByVerbId(verbId),
            Constants.Imperative => GetImperativeByVerbId(verbId),
            _ => null
        };
    }

    protected override IQueryable<Verb> MakeInclusions()
    {
        return base.MakeInclusions()
            .Include(v => v.Infinitive)
            .Include(v => v.VerbModels);
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
