using HebrewVerb.Application.Interfaces;
using HebrewVerb.Core;

namespace HebrewVerb.Infrastructure.Repositories;

public class ShoreshRepository : Repository<Shoresh, int>, IShoreshRepository
{
    public ShoreshRepository(AppDbContext context) : base(context)
    { }
}
