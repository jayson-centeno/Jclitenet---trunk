using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreFramework4.Model;

namespace CoreFramework4.Infrastructure.Repository
{
    public interface IAlbumRepository : IRepository<Album>
    {
        Album GetAlbumWithPhotos(Guid? uid);
        void DeleteByUID(Guid uid);
    }
}
