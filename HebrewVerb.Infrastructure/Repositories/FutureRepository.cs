using HebrewVerb.Application.Interfaces.Repositories;
using HebrewVerb.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HebrewVerb.Infrastructure.Repositories;

public class FutureRepository : Repository<Future, int>, IFutureRepository
{
    public FutureRepository(AppDbContext context) : base(context)
    { }

    protected override IQueryable<Future> MakeInclusions()
    {
        return base.MakeInclusions()
            .Include(p => p.MS1)
            .Include(p => p.MP1)
            .Include(p => p.MS2)
            .Include(p => p.FS2)
            .Include(p => p.MP2)
            .Include(p => p.MS3)
            .Include(p => p.MP3);
    }
}
