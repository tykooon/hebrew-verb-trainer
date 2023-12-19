using HebrewVerb.Application;

namespace HebrewVerb.Infrastructure;

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

    public void Update(TEntity entity) => _appDbContext.Update(entity);
}
