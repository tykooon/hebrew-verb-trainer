using HebrewVerb.Application.Models;
using HebrewVerb.Domain.Entities;
using HebrewVerb.Domain.Interfaces;
using HebrewVerb.SharedKernel.Enums;

namespace HebrewVerb.Application.Interfaces.Repositories;

public interface IVerbRepository : IRepository<Verb, int>
{
    IConjugation? GetTenseByVerbId(int verbId, Zman zman);
    Task<IEnumerable<Verb>> GetFilteredVerbs(Filter filter, int randomTake);
    Task<Verb?> GetVerbFullDataByIdAsync(int verbId);
}
