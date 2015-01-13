using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoreFramework4;
using CoreFramework4.Model;
using CoreFramework4.Infrastructure.Repository;
using CoreFramework4.Infrastructure.Services;
using jclitenet.Models;

namespace jclitenet.Controllers
{
    public class PhotoGalleryController : SiteBaseController
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly IAlbumService _albumService;

        public PhotoGalleryController(IAlbumRepository albumRepository,
                                      IAlbumService albumService)
        {
            _albumRepository = albumRepository;
            _albumService = albumService;
        }

        public ActionResult Index()
        {
            var albumModels = _albumRepository.GetAll().OrderBy(a => a.ID)
                                          .Select(a => new AlbumModel()
                                          {
                                              ID = a.ID,
                                              UID = a.UID,
                                              Name = a.Name,
                                              Comment = a.Comment,
                                              CreatedBy = a.CreatedBy,
                                              DateCreated = a.DateCreated
                                          });
            return View(albumModels);
        }

        public ActionResult Album(int id)
        {
            var album = _albumService.GetAllAlbumWithPhotos().FirstOrDefault(a => a.ID == id);

            var albumModel = new AlbumModel() 
            {
                ID = album.ID,
                UID = album.UID,
                Name = album.Name,
                Comment = album.Comment,
                CreatedBy = album.CreatedBy,
                DateCreated = album.DateCreated,
                Photos = album
                        .Photos
                        .OrderBy(p => p.ID)
                        .Select(p => new PhotoModel() {
                                ID = p.ID,
                                FileName = p.FileName,
                                Name = p.Name,
                                CreatedBy = p.CreatedBy,
                                IsDeleted = p.IsDeleted,
                                UID = p.UID
                         })
            };

            return View(albumModel);
        }
    }
}
