using HebrewVerb.Application.Entities;
using HebrewVerb.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HebrewVerb.Infrastructure.Repositories;

public class FilterSnapshotRepository : Repository<FilterSnapshot, int>, IFilterSnapshotRepository
{
    public FilterSnapshotRepository(AppDbContext context) : base(context)
    { }

    protected override IQueryable<FilterSnapshot> MakeInclusions()
    {
        return base.MakeInclusions().Include(f => f.AppUser);
    }
}
