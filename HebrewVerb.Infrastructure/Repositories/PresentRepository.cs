using HebrewVerb.Application.Interfaces.Repositories;
using HebrewVerb.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HebrewVerb.Infrastructure.Repositories;

public class PresentRepository : Repository<Present, int>, IPresentRepository
{
    public PresentRepository(AppDbContext context) : base(context)
    { }

    protected override IQueryable<Present> MakeInclusions()
    {
        return base.MakeInclusions()
            .Include(p => p.MS)
            .Include(p => p.MP)
            .Include(p => p.FS)
            .Include(p => p.FP);
    }
}
