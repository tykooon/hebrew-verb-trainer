using HebrewVerb.Application;
using HebrewVerb.Core;

namespace HebrewVerb.Infrastructure;

public class VerbRepository : Repository<Verb, int>, IVerbRepository
{
    public VerbRepository(AppDbContext context) : base(context)
    { }
}
