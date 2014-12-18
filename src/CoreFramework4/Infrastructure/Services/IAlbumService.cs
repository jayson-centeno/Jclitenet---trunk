using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreFramework4.Model;

namespace CoreFramework4.Infrastructure.Services
{
    public interface IAlbumService : IService
    {
        IEnumerable<Album> GetAllAlbum();
        IEnumerable<Album> GetAllAlbumWithPhotos();
    }
}
