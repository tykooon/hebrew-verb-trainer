﻿using HebrewVerb.SharedKernel.Abstractions;
using System.Linq.Expressions;

namespace HebrewVerb.Application.Interfaces.Repositories;

public interface IRepository<TEntity, TKey>
    where TEntity : BaseEntity<TKey>
    where TKey : struct
{
    //Queries
    public IEnumerable<TEntity> GetAll();
    public Task<List<TEntity>> GetAllAsync();

    public TEntity? GetById(TKey key);
    public Task<TEntity?> GetByIdAsync(TKey key);
        
    public IEnumerable<TEntity> FindAllBy(Expression<Func<TEntity, bool>> predicate);

    //Commands
    public void Add(TEntity entity);
    public void Update(TEntity entity);
    public void Delete(TEntity entity);
}
