using System.Linq.Expressions;

namespace HebrewVerb.Application.Identity;

public interface IIdRepository<TEntity, TKey> where TEntity : class where TKey : struct
{
    public TEntity? Find(TKey Id);

    TEntity? Find(Expression<Func<TEntity, bool>> predicate);

    public void Add(TEntity entity);

    public void Update(TEntity entity);

    public void Delete(TEntity entity);
}
