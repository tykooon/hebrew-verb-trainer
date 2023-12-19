using HebrewVerb.Application;
using HebrewVerb.Core;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace HebrewVerb.Infrastructure;

public abstract class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
    where TEntity : Entity<TKey>
    where TKey : struct
{
    protected AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public TKey? Add(TEntity entity)
    {
        var res = _context.Set<TEntity>().Add(entity);
        _context.SaveChanges();
        Console.WriteLine(res.DebugView.LongView);
        return res.IsKeySet ? entity.Id : null;
    }

    public TEntity GetAll()
    {
        throw new NotImplementedException();
    }

    public TEntity? GetById(TKey key)
    {
        return _context.Set<TEntity>().Find(key);
    }
}
