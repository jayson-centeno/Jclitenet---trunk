using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreFramework4.Infrastructure.Repository;
using CoreFramework4.Model;

namespace CoreFramework4.Implementations.Repository
{
    public class PhotoRepository : RepositoryBase<Photo>, IPhotoRepository
    {
        public void DeleteByUID(Guid uid)
        {
            Delete(Get(uid));
        }
    }
}
