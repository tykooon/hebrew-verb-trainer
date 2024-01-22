using HebrewVerb.Application.Interfaces.Repositories;
using HebrewVerb.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HebrewVerb.Infrastructure.Repositories;

public class ImperativeRepository : Repository<Imperative, int>, IImperativeRepository
{
    public ImperativeRepository(AppDbContext context) : base(context)
    { }

    protected override IQueryable<Imperative> MakeInclusions()
    {
        return base.MakeInclusions()
            .Include(p => p.MS)
            .Include(p => p.MP)
            .Include(p => p.FS);
    }
}
