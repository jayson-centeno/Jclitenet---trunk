using CoreFramework4.Infrastructure.Repository;
using CoreFramework4.Model;
using System;

namespace CoreFramework4.Implementations.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public User GetByGuidID(Guid PKID)
        {
            return this.First(u => u.UID == PKID);
        }

        public User GetByEmail(string email)
        {
            return this.First(u => u.Email == email);
        }
    }
}
