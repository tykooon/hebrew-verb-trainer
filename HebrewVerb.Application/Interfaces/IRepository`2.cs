using HebrewVerb.Core;

namespace HebrewVerb.Application.Interfaces;

public interface IRepository<TEntity, TKey>
    where TEntity : Entity<TKey>
    where TKey : struct
{
    public TEntity GetAll();
    public TEntity? GetById(TKey key);
    public TKey? Add(TEntity entity);
}
