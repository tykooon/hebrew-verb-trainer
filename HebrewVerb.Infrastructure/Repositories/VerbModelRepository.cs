using HebrewVerb.Application.Interfaces;
using HebrewVerb.Core;

namespace HebrewVerb.Infrastructure.Repositories;

public class VerbModelRepository : Repository<VerbModel, int>, IVerbModelRepository
{
    public VerbModelRepository(AppDbContext context) : base(context)
    { }
}
