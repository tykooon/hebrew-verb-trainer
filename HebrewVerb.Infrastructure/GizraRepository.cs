using HebrewVerb.Application;
using HebrewVerb.Core;

namespace HebrewVerb.Infrastructure;

public class GizraRepository : Repository<Gizra, int>, IGizraRepository
{
    public GizraRepository(AppDbContext context) : base(context)
    { }
}
