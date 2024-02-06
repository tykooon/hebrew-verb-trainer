using HebrewVerb.Domain.Entities;

namespace HebrewVerb.Application.Interfaces.Repositories;

public interface IPrepositionRepository : IRepository<Preposition, int>
{
    public Preposition? GetAllFormsById(int id);
}
