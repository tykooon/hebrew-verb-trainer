using HebrewVerb.Application.Interfaces.Repositories;
using HebrewVerb.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HebrewVerb.Infrastructure.Repositories;

public class PrepositionRepository : Repository<Preposition, int>, IPrepositionRepository
{
    public PrepositionRepository(AppDbContext context) : base(context)
    { }

    public Preposition? GetAllFormsById(int id)
    {
        return MakeInclusions()
            .Include(p => p.MS1)
            .Include(p => p.MP1)
            .Include(p => p.MS2)
            .Include(p => p.MP2)
            .Include(p => p.FS2)
            .Include(p => p.FP2)
            .Include(p => p.MS3)
            .Include(p => p.MP3)
            .Include(p => p.FS3)
            .Include(p => p.FP3)
            .FirstOrDefault(p => p.Id == id);
    }

    protected override IQueryable<Preposition> MakeInclusions()
    {
        return base.MakeInclusions().Include(p => p.BaseForm);
    }
}
