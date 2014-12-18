using CoreFramework4.Model;
using System;

namespace CoreFramework4.Infrastructure.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByGuidID(Guid PKID);
        User GetByEmail(string email);
    }
}
