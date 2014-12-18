using CoreFramework4.Model;
using CoreFramework4.Infrastructure.Repository;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;

namespace CoreFramework4.Implementations.Repository
{
    public class ChangeLogRepository : RepositoryBase<ChangeLog>, IChangeLogRepository
    {
        public void Register(ChangeLog chageLog)
        {
            Add(chageLog);
            SaveChanges();
        }

        public async Task<DbQuery<ChangeLog>> GetAllAsync()
        {
            return await Task.Run(() => GetAll());
        }
    }
}
