using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Threading.Tasks;

namespace CoreFramework4.Infrastructure.Repository
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        DbQuery<TEntity> GetQuery();
        DbQuery<T> GetQuery<T>() where T : class;
        DbQuery<TEntity> GetAll();
        IList <TEntity> GetAllList();
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        IList<TEntity> FindList(Expression<Func<TEntity, bool>> predicate);

        TEntity Single(Expression<Func<TEntity, bool>> predicate);
        TEntity First(Expression<Func<TEntity, bool>> predicate);
        void Add(TEntity entity);
        void Delete(TEntity entity);
        void Attach(TEntity entity);
        TEntity Get(Guid id);
        TEntity Get(int id);
        void SaveChanges();

        #region Async

        Task<IList<TEntity>> GetAllAsyncList();
        Task<IList<TEntity>> FindAsyncList(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetAsync(int id);
        Task<TEntity> GetAsync(Guid id);
        void SaveChangesAsync();

        #endregion

    }
}
