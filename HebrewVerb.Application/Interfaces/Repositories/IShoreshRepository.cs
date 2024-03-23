using HebrewVerb.Domain.Entities;

namespace HebrewVerb.Application.Interfaces.Repositories;

public interface IShoreshRepository : IRepository<Shoresh, int>
{
    Task<Shoresh?> GetByNameAsync(string name);
}
