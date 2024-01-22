using HebrewVerb.Application.Interfaces.Repositories;
using HebrewVerb.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HebrewVerb.Infrastructure.Repositories;

public class WordFormRepository : Repository<WordForm, int>, IWordFormRepository
{
    public WordFormRepository(AppDbContext context) : base(context)
    { }

    protected override IQueryable<WordForm> MakeInclusions()
    {
        return base.MakeInclusions();
    }
}
