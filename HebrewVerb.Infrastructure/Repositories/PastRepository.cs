using HebrewVerb.Application.Interfaces.Repositories;
using HebrewVerb.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HebrewVerb.Infrastructure.Repositories;

public class PastRepository : Repository<Past, int>, IPastRepository
{
    public PastRepository(AppDbContext context) : base(context)
    { }

    protected override IQueryable<Past> MakeInclusions()
    {
        return base.MakeInclusions()
            .Include(p => p.MS1)
            .Include(p => p.MP1)
            .Include(p => p.MS2)
            .Include(p => p.FS2)
            .Include(p => p.MP2)
            .Include(p => p.FP2)
            .Include(p => p.MS3)
            .Include(p => p.FS3)
            .Include(p => p.MP3);
    }
}
