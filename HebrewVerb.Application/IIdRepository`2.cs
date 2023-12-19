namespace HebrewVerb.Application;

public interface IIdRepository<TEntity, TKey> where TEntity : class where TKey : struct
{
    public TEntity? Find(TKey Id);

    //AppUser Find(Expression<Func<AppUser, bool>> predicate);

    public void Add(TEntity entity);

    public void Update(TEntity entity);

    public void Delete(TEntity entity);
}
