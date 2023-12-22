using HebrewVerb.Application.Interfaces;
using HebrewVerb.Core;

namespace HebrewVerb.Infrastructure.Repositories;

public class VerbRepository : Repository<Verb, int>, IVerbRepository
{
    public VerbRepository(AppDbContext context) : base(context)
    { }
}
