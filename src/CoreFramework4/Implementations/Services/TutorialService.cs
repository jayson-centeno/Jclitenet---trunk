using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreFramework4.Infrastructure.Repository;
using CoreFramework4.Infrastructure.Services;
using CoreFramework4.Model;
using CoreFramework4.Tool;
using System.Data.Entity;

namespace CoreFramework4.Implementations.Services
{
    public class TutorialService : ITutorialService
    {
        private ITutorialRepository _tutorialRepository;

        public TutorialService(ITutorialRepository tutorialRepository)
        {
            _tutorialRepository = tutorialRepository;
        }

        public async Task<IEnumerable<Tutorial>> GetAllTutorialWithCategoryWithCommentsAsync(int count = 0)
        {
            return await _tutorialRepository.GetQuery()
                                            .Include("TutorialCategory")
                                            .Include("Comments")
                                            .Take(count)
                                            .OrderBy(x => Guid.NewGuid())
                                            .ToListAsync();
        }

        public IEnumerable<Tutorial> GetAllTutorial()
        {
            string key = "ALLTUTORIALS";
            var applicationCachingServices = CachingFactory.ApplicationCachingEngineInstance;
            var lst = applicationCachingServices.GetItem<IEnumerable<Tutorial>>(key);
            if (lst == null || lst.Count() == 0)
            {
                lst = _tutorialRepository.GetQuery()
                                            .ToList();

                applicationCachingServices.AddItem(key, lst, 5);
            }

            return lst;
        }

        public IEnumerable<Tutorial> GetAllTutorialWithCategory()
        {
            string key = "ALLTUTORIALSWITHCATEGORY";
            var applicationCachingServices = CachingFactory.ApplicationCachingEngineInstance;
            var lst = applicationCachingServices.GetItem<IEnumerable<Tutorial>>(key);
            if (lst == null || lst.Count() == 0)
            {
                lst = _tutorialRepository.GetQuery()
                                            .Include("TutorialCategory")
                                            .ToList();

                applicationCachingServices.AddItem(key, lst, 5);
            }

            return lst;
        }
    }
}