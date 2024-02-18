using HebrewVerb.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using HebrewVerb.SharedKernel.Abstractions;

namespace HebrewVerb.Infrastructure.Repositories;

public abstract class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
    where TEntity : BaseEntity<TKey>
    where TKey : struct
{
    protected DbSet<TEntity> DbSet;

    public Repository(AppDbContext context)
    {
        DbSet = context.Set<TEntity>();
    }

    public void Add(TEntity entity) => DbSet.Add(entity);

    public void Delete(TEntity entity) => DbSet.Remove(entity);

    public void Update(TEntity entity) => DbSet.Update(entity);

    public IEnumerable<TEntity> FindAllBy(Expression<Func<TEntity, bool>> predicate)
    {
        return MakeInclusions().Where(predicate);
    }

    public IEnumerable<TEntity> GetAll()
    {
        return MakeInclusions().ToList();
    }

    public TEntity? GetById(TKey id)
    {
        return MakeInclusions().FirstOrDefault(x => x.Id.Equals(id));
    }

    protected virtual IQueryable<TEntity> MakeInclusions() => DbSet;
}
