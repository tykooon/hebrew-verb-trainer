using HebrewVerb.Domain.Entities;
using HebrewVerb.Domain.Enums;
using HebrewVerb.Domain.Interfaces;

namespace HebrewVerb.Application.Interfaces.Repositories;

public interface IVerbRepository : IRepository<Verb, int>
{
    IConjugation? GetTenseByVerbId(int verbId, Zman zman);
}
