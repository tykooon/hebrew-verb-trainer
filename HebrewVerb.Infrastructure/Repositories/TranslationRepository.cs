using HebrewVerb.Application.Interfaces.Repositories;
using HebrewVerb.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HebrewVerb.Infrastructure.Repositories;

public class TranslationRepository : Repository<Translation, int>, ITranslationRepository
{
    public TranslationRepository(AppDbContext context) : base(context)
    { }

    protected override IQueryable<Translation> MakeInclusions()
    {
        return base.MakeInclusions();
    }
}
