using HebrewVerb.Application.Interfaces.Repositories;
using HebrewVerb.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HebrewVerb.Infrastructure.Repositories;

public class TranslationRepository(AppDbContext context) : Repository<Translation, int>(context), ITranslationRepository
{
    //public IEnumerable<Translation> GetVerbTranslations(int verbId)
    //{
    //    return MakeInclusions().Where(tr => tr.VerbId == verbId);
    //}

    protected override IQueryable<Translation> MakeInclusions()
    {
        return base.MakeInclusions().Include(tr => tr.Prepositions).ThenInclude(pr => pr.BaseForm);
    }
}
