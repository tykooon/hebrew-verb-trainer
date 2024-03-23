using HebrewVerb.Application.Interfaces.Repositories;
using HebrewVerb.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HebrewVerb.Infrastructure.Repositories;

public class ShoreshRepository(AppDbContext context) :
    Repository<Shoresh, int>(context), IShoreshRepository
{
    public Task<Shoresh?> GetByNameAsync(string name)
    {
        return MakeInclusions().FirstOrDefaultAsync(x => x.Short.Equals(name));
    }

    protected override IQueryable<Shoresh> MakeInclusions()
    {
        return base.MakeInclusions().Include(sh => sh.Verbs);
    }
}
