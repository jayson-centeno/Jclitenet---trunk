using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreFramework4.Model;

namespace CoreFramework4.Infrastructure.Repository
{
    public interface IChangeLogRepository : IRepository<ChangeLog>
    {
        void Register(ChangeLog chageLog);
    }
}
