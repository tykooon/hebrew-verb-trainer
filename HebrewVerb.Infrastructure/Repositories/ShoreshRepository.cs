using HebrewVerb.Application.Interfaces.Repositories;
using HebrewVerb.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HebrewVerb.Infrastructure.Repositories;

public class ShoreshRepository : Repository<Shoresh, int>, IShoreshRepository
{
    public ShoreshRepository(AppDbContext context) : base(context)
    { }

    protected override IQueryable<Shoresh> MakeInclusions()
    {
        return base.MakeInclusions().Include(sh => sh.Verbs);
    }
}
