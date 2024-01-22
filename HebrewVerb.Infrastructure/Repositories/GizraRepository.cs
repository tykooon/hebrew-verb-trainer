using HebrewVerb.Application.Interfaces.Repositories;
using HebrewVerb.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HebrewVerb.Infrastructure.Repositories;

public class GizraRepository : Repository<Gizra, int>, IGizraRepository
{
    public GizraRepository(AppDbContext context) : base(context)
    { }

    protected override IQueryable<Gizra> MakeInclusions()
    {
        return base.MakeInclusions().Include(g => g.Shoreshes);
    }
}
