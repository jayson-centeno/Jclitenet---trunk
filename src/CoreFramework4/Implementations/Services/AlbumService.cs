using System.Collections.Generic;
using System.Linq;
using CoreFramework4.Model;
using CoreFramework4.Model;
using CoreFramework4.Infrastructure.Repository;
using CoreFramework4.Infrastructure.Services;
using System.Data.Entity;
using System.Threading.Tasks;

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
                                   .ToList();
        }

        public IEnumerable<Album> GetAllAlbumWithPhotos()
        {
            return _albumRepository.GetAll()
                                   .Include("Photos")
                                   .ToList();
        }

        public async Task<IEnumerable<Album>> GetAllAlbumAsync()
        {
            return await _albumRepository.GetAll()
                                  .Include("Photos")
                                  .ToListAsync();
        }
    }
}
