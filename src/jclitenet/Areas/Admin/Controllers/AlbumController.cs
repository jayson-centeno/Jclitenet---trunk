using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using CoreFramework4;
using CoreFramework4.Implementations.Security;
using CoreFramework4.Infrastructure.Repository;
using CoreFramework4.Model;
using jclitenet.Models;

namespace jclitenet.Areas.Admin.Controllers
{
    [SiteAuthorization]
	public class AlbumController : Controller
	{
		public ActionResult Index()
		{
			var albumRepository = ServiceFactory.GetInstance<IAlbumRepository>();
			var allAlbumModels = albumRepository.GetAll().Select(a => new AlbumModel
			{
				UID = a.UID,
				Name = a.Name,
			});

			return View(allAlbumModels);
		}

		public ActionResult Edit(Guid? id)
		{
			var albumRepository = ServiceFactory.GetInstance<IAlbumRepository>();
			var album = albumRepository.GetAlbumWithPhotos(id);

			if (album != null)
			{
				var albumModel = new AlbumModel()
				{
					Name = album.Name,
					UID = album.UID,
					Photos = album.Photos.Select(p => new PhotoModel() { 
						FileName = p.FileName,
						Name = p.Name,
						UID = p.UID
					})
				};

				return View(albumModel);
			}

			return View();
		}

		[HttpPost]
		public ActionResult Create(AlbumModel model)
		{
			if (ModelState.IsValid)
			{
				var albumRepostory = ServiceFactory.GetInstance<IAlbumRepository>();

				Guid newId = Guid.NewGuid();
				DateTime createdDate = DateTime.Now;

				var newAlbum = new Album() {
					Name = model.Name,
					DateCreated = createdDate,
					UID = newId,
					Photos = new List<Photo>()
				};

				albumRepostory.Add(newAlbum);

				if (Request.Files.Count > 0)
				{
					for (int i = 0; i < Request.Files.Count; i++)
					{
						var postedFile = this.Request.Files[i];
						if (postedFile.ContentLength == 0)
						{
							continue;
						}

						string newAlbumDirectory = Path.Combine(
							AppDomain.CurrentDomain.BaseDirectory, @"Content\photos\" + newId.ToString().ToUpper());

						if (!Directory.Exists(newAlbumDirectory))
						{
							Directory.CreateDirectory(newAlbumDirectory);
						}

						string savedFileName = Path.GetFileName(postedFile.FileName);
						string extension = Path.GetExtension(savedFileName);
						string fileName = String.Format("{0}{1}{2}", "image", i, extension);

						newAlbum.Photos.Add(new Photo()
						{
							FileName = fileName,
							Name = "image" + i,
							DateCreated = createdDate,
							UID = Guid.NewGuid()
						});

						postedFile.SaveAs(String.Format("{0}\\{1}", newAlbumDirectory, fileName));
					}

				}

				albumRepostory.SaveChanges();
				return RedirectToAction("Edit", "Album", new { id = newId });
			}

			return View();
		}

		public ActionResult Create()
		{
			return View();
		}

		public void Delete(Guid id)
		{
			var albumRepository = ServiceFactory.GetInstance<IAlbumRepository>();
			var album = albumRepository.GetAlbumWithPhotos(id);

            if (album == null) return;

            var photoRepository = ServiceFactory.GetInstance<IPhotoRepository>();
            var photoIds = album.Photos.Select(p => p.UID).ToArray();

            foreach (var photoId in photoIds)
			{
                photoRepository.DeleteByUID(photoId);
			}

            albumRepository.Delete(album);

            string albumPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory, @"Content\photos\" + id.ToString().ToUpper());

            try
            {
                if (Directory.Exists(albumPath))
                {
                    Directory.Delete(albumPath, true);
                }

                photoRepository.SaveChanges();
                albumRepository.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            
		}

	}
}
