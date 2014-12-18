using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity.Infrastructure;

namespace CoreFramework4.Infrastructure.Repository
{
    public interface IRepository<TEntity> : IDisposable where TEntity: class 
    {
        DbQuery<TEntity> GetQuery();
        DbQuery<TEntity> GetAll();
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        TEntity Single(Expression<Func<TEntity, bool>> predicate);
        TEntity First(Expression<Func<TEntity, bool>> predicate);
        void Add(TEntity entity);
        void Delete(TEntity entity);
        void Attach(TEntity entity);
        TEntity Get(Guid id);
        TEntity Get(int id);
        void SaveChanges();
    }
}
