using HebrewVerb.Application.Interfaces.Repositories;
using HebrewVerb.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HebrewVerb.Infrastructure.Repositories;

public class VerbModelRepository : Repository<VerbModel, int>, IVerbModelRepository
{
    public VerbModelRepository(AppDbContext context) : base(context)
    { }

    protected override IQueryable<VerbModel> MakeInclusions()
    {
        return base.MakeInclusions().Include(vm => vm.Verbs);
    }
}
