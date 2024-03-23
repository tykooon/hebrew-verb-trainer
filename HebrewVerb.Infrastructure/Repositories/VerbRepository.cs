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
    private readonly DbSet<Past> _pasts = context.Pasts;
    private readonly DbSet<Present> _presents = context.Presents;
    private readonly DbSet<Future> _futures = context.Futures;
    private readonly DbSet<Imperative> _imperatives = context.Imperatives;

    public IConjugation? GetTenseByVerbId(int verbId, Zman zman)
    {
        var verb = DbSet.Find(verbId);
        if (verb == null)
        {
            return null;
        }

        return zman.Name switch
        {
            TenseName.Past => GetPastById(verb.PastId),
            TenseName.Present => GetPresentById(verb.PresentId),
            TenseName.Future => GetFutureById(verb.FutureId),
            TenseName.Imperative => GetImperativeById(verb.ImperativeId),
            _ => null
        };
    }

    public async Task<IEnumerable<Verb>> GetFilteredVerbs(Filter filter, int randomTake = 0)
    {
        var binyans = filter.Binyans.GetTagsFromNames(Binyan.List).GetFlagSum();
        var gizras = Gizra.GetTagsFromIds(filter.Gizras).GetFlagSum();
        var verbModels = VerbModel.GetTagsFromIds(filter.VerbModels).GetFlagSum();
        var verbTags = VerbTag.GetTagsFromIds(filter.VerbTags).GetFlagSum();

        var verbs = DbSet.FromSql($"""
                     select *
                     from [Verbs]
                     where (({binyans} == 0) or (1 << Binyan) & {binyans} != 0) 
                        and (({gizras} == 0) or Gizras & {gizras} != 0)
                        and (({verbModels} == 0) or VerbModels & {verbModels} != 0)
                        and (({verbTags} == 0) or Tags & {verbTags} != 0)
                     """);
        if(randomTake != 0)
        {
            verbs = verbs.OrderBy(v => EF.Functions.Random()).Take(randomTake);
        }

        var verbIds = verbs.Select(v => v.Id);

        var res = await MakeInclusions().Where(v => verbIds.Contains(v.Id)).ToListAsync();

        return res;
    }

    public async Task<Verb?> GetVerbFullDataByIdAsync(int verbId)
    {
        var result = await MakeInclusions().FirstOrDefaultAsync(v => v.Id == verbId);
        if (result == null)
        {
            return null;
        }

        result.Present = GetPresentById(result.PresentId);
        result.Past = GetPastById(result.PastId);
        result.Future = GetFutureById(result.FutureId);
        result.Imperative = GetImperativeById(result.ImperativeId);

        return result;
    }

    protected override IQueryable<Verb> MakeInclusions()
    {
        return base.MakeInclusions()
            .Include(v => v.Infinitive)
            .Include(v => v.Shoresh)
            .Include(v => v.Translations).ThenInclude(tr => tr.Prepositions).ThenInclude(pr => pr.BaseForm);
    }

    private Past? GetPastById(int? pastId)
    {
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

    private Present? GetPresentById(int? presentId)
    {
        return presentId != null ? _presents?
            .Include(p => p.MS)
            .Include(p => p.MP)
            .Include(p => p.FS)
            .Include(p => p.FP)
            .FirstOrDefault(p => p.Id == presentId)
            : null;
    }

    private Future? GetFutureById(int? futureId)
    {
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

    private Imperative? GetImperativeById(int? imperativeId)
    {
        return imperativeId != null ? _imperatives?
            .Include(p => p.MS)
            .Include(p => p.MP)
            .Include(p => p.FS)
            .FirstOrDefault(p => p.Id == imperativeId)
            : null;
    }
}
