using HebrewVerb.Application;
using HebrewVerb.Core;

namespace HebrewVerb.Infrastructure;

public class ShoreshRepository : Repository<Shoresh, int>, IShoreshRepository
{
    public ShoreshRepository(AppDbContext context) : base(context)
    { }
}
