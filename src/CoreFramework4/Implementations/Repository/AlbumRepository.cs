using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreFramework4.Implementations.Repository;
using CoreFramework4.Model;
using CoreFramework4.Infrastructure.Repository;

namespace CoreFramework4.Implementations.Repository
{
    public class AlbumRepository : RepositoryBase<Album>, IAlbumRepository
    {
        public Album GetAlbumWithPhotos(Guid? uid)
        {
            return GetQuery().Include("Photos").FirstOrDefault(a => a.UID == uid);
        }

        public void DeleteByUID(Guid uid)
        {
            Delete(Get(uid));
        }
    }
}
