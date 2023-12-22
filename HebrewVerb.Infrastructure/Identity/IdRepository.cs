using HebrewVerb.Application.Identity;
using System.Linq.Expressions;

namespace HebrewVerb.Infrastructure.Identity;

public abstract class IdRepository<TEntity, TKey> : IIdRepository<TEntity, TKey>
    where TEntity : class
    where TKey : struct
{
    protected AppDbContext _appDbContext;

    public IdRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public void Add(TEntity entity) => _appDbContext.Add(entity);

    public void Delete(TEntity entity) => _appDbContext.Remove(entity);

    public TEntity? Find(TKey Id) => _appDbContext.Set<TEntity>().Find(Id);

    public TEntity? Find(Expression<Func<TEntity, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public void Update(TEntity entity) => _appDbContext.Update(entity);
}
