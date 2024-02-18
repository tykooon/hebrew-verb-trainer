using HebrewVerb.Application.Entities;
using HebrewVerb.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HebrewVerb.Infrastructure.Repositories;

public class FilterRepository : Repository<AppFilter, int>, IFilterRepository
{
    public FilterRepository(AppDbContext context) : base(context)
    { }

    protected override IQueryable<AppFilter> MakeInclusions()
    {
        return base.MakeInclusions().Include(f => f.AppUser);
    }
}
