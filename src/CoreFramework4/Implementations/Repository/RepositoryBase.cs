using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using CoreFramework4.Infrastructure.Repository;

namespace CoreFramework4.Implementations.Repository
{
    public class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class 
    {
        private DbManager _dbManager;

        public RepositoryBase()
        {
            _dbManager = ServiceFactory.GetInstance<DbManager>();
            _dbManager.EnableLazyLoading = false;
        }

        public bool EnableLazyLoading
        { 
            get { return _dbManager.EnableLazyLoading; }
            set { _dbManager.EnableLazyLoading = value; }
        }

        public DbQuery<TEntity> GetQuery()
        {
            return _dbManager.Set<TEntity>();
        }

        public DbQuery<T> GetQuery<T>() where T :class
        {
            return _dbManager.Set<T>();
        }

        public DbQuery<TEntity> GetAll()
        {
            return GetQuery();
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return GetQuery().Where(predicate);
        }

        public TEntity Single(Expression<Func<TEntity, bool>> predicate) 
        {
            return GetQuery().Single(predicate);
        }

        /// <summary>
        /// Used FirstOrDefault
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public TEntity First(Expression<Func<TEntity, bool>> predicate) 
        {
            return GetQuery().Where(predicate).FirstOrDefault();
        }

        public void Add(TEntity entity) 
        {
            _dbManager.Set<TEntity>().Add(entity);
        }

        public void Delete(TEntity entity) 
        {
            _dbManager.Set<TEntity>().Remove(entity);
        }

        public void Attach(TEntity entity) 
        {
            _dbManager.Set<TEntity>().Attach(entity);
        }

        public TEntity Get(int id)
        {
            return _dbManager.Set<TEntity>().Find(id);
        }

        public TEntity Get(Guid id)
        {
            return _dbManager.Set<TEntity>().Find(id);
        }

        public void SaveChanges()
        {
            _dbManager.SaveChanges();
        }

        public void Dispose()
        {
            _dbManager.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}
