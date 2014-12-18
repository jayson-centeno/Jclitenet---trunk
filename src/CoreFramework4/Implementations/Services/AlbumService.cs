using System.Collections.Generic;
using System.Linq;
using CoreFramework4.Model;
using CoreFramework4.Infrastructure.Repository;
using CoreFramework4.Infrastructure.Services;

namespace CoreFramework4.Implementations.Services
{
    public class AlbumService : IAlbumService
    {
        private IAlbumRepository _albumRepository;

        public AlbumService(IAlbumRepository albumRepository)
        {
            _albumRepository = albumRepository;
        }

        public IEnumerable<Album> GetAllAlbum()
        {
            return _albumRepository.GetAll()
                                   .AsParallel()
                                   .ToList();
        }

        public IEnumerable<Album> GetAllAlbumWithPhotos()
        {
            return _albumRepository.GetAll()
                                   .Include("Photos")
                                   .AsParallel()
                                   .ToList();
        }
    }
}
