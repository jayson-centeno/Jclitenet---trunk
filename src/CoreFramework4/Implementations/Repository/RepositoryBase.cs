using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using CoreFramework4.Infrastructure.Repository;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Collections.Generic;

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

        public DbQuery<T> GetQuery<T>() where T : class
        {
            return _dbManager.Set<T>();
        }

        public DbQuery<TEntity> GetAll()
        {
            return GetQuery();
        }

        public IList<TEntity> GetAllList()
        {
            return GetQuery().ToList();
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return GetQuery().Where(predicate);
        }

        public IList<TEntity> FindList(Expression<Func<TEntity, bool>> predicate)
        {
            return GetQuery().Where(predicate).ToList();
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

        #region Async

        public async Task<IList<TEntity>> GetAllAsyncList()
        {
            return await GetAll().ToListAsync();
        }

        public async Task<IList<TEntity>> FindAsyncList(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetQuery().Where(predicate).ToListAsync();
        }

        public async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetQuery().SingleOrDefaultAsync(predicate);
        }

        public async Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetQuery().FirstOrDefaultAsync(predicate);
        }

        public async Task<TEntity> GetAsync(int id)
        {
            return await _dbManager.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> GetAsync(Guid id)
        {
            return await _dbManager.Set<TEntity>().FindAsync(id);
        }

        public async void SaveChangesAsync()
        {
            await _dbManager.SaveChangesAsync();
        }

        #endregion
    }
}
